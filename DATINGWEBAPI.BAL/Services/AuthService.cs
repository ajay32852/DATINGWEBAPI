using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.BLL.Utilities.CustomExceptions;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
namespace DATINGWEBAPI.BAL.Services
{
    public class AuthService : IAuthService
    {

        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AuthService> _localizer;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IVerificationCodeRepository _verificationCodeRepository;

        public AuthService(
            IMapper mapper,
            IStringLocalizer<AuthService> localizer,
            IConfiguration configuration,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IVerificationCodeRepository verificationCodeRepository
            )
        {
            _mapper = mapper;
            _localizer = localizer;
            _configuration = configuration;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _verificationCodeRepository = verificationCodeRepository;
        }

        public async Task<OTPMobileNoResponse?> RequestOTP(RequestOTPDTO requestOTPDTO)
        {
            var phoneNumber = requestOTPDTO.MobileNumber.ToString();
            var user = await _userRepository.GetUserByMobileAsync(phoneNumber);
            if (user == null)
            {
                user = new USER
                {
                    PHONENUMBER = phoneNumber,
                    FIRSTNAME = "Guest",
                    LASTNAME = "User",
                    ROLE = "user",
                    CREATEDAT = DateTime.UtcNow,
                    LASTLOGIN = DateTime.UtcNow,
                    ISDELETED = false,
                    ISBLOCKED = false,
                    BIRTHDAY=DateTime.UtcNow, 
                    GENDER="Other",
                    LATITUDE=0.0,
                    LONGITUDE=0.0,
                    ISPROFILECOMPLETE= false,
                    AGE = 0,
                    LOCATION = "Unknown",
                    PROFILEIMAGEURL= "https://avatar.iran.liara.run/public/22",
                    BIO=string.Empty,
                };
                await _userRepository.AddNewUser(user);
            }
            if (user.ISBLOCKED)
            {
                throw new UserBlockedException(_localizer[ResponseMessage.BlockedUser.ToString()]);
            }
            var otp = Common.OTPGenerate();
            var verificationCode = new VERIFICATIONCODE
            {
                USERID = user.USERID,
                PHONENUMBER = user.PHONENUMBER,
                CODE = otp.ToString(),
                EXPIRESAT = DateTime.Now.AddMinutes(5), 
                CREATEDAT = DateTime.Now,            
                ISUSED = false
            };
            await _verificationCodeRepository.saveVerificationCode(verificationCode);
            return new OTPMobileNoResponse
            {
                MobileNumber = user.PHONENUMBER,
                OTP = otp
            };
        }
        public async Task<UserLoginDTO?> VerifyOTP(LoginRequestDTO loginRequestDTO)
        {
            var userResponse = await _userRepository.GetUserByMobileAsync(loginRequestDTO.MobileNumber.ToString());
            var user = _mapper.Map<UserDTO>(userResponse);
            if (user == null)
            {
                throw new UserNotFoundException(_localizer[ResponseMessage.InvalidUser.ToString()]);
            }
            var userotp= await _verificationCodeRepository.getVerificationCodeByPhoneNumber(loginRequestDTO.MobileNumber.ToString());
            var mapData=_mapper.Map<VerificationCodeDTO>(userotp);
            if (mapData == null)
            {
                throw new UserOTPInvalidException(_localizer[ResponseMessage.InvalidOTP.ToString()]);
            }
           
            if (mapData.IsUsed || mapData.ExpiresAt < DateTime.UtcNow || mapData.Code != loginRequestDTO.OTP.ToString())
            {
                throw new UserOTPExpiredException(_localizer[ResponseMessage.OTPExpire.ToString()]);
            }
            mapData.IsUsed = true;
            var updateOTP =_mapper.Map<VERIFICATIONCODE>(mapData);
            var updateOTPState= await _verificationCodeRepository.updateVerificationCode(updateOTP);
            var mappedUserData = _mapper.Map<UserDTO>(user);
            if (mappedUserData.IsBlocked) 
            {
                throw new UserBlockedException(_localizer[ResponseMessage.BlockedUser.ToString()]);
            }
            if (!string.IsNullOrEmpty(loginRequestDTO.FirstName))
            {
                user.FirstName = loginRequestDTO.FirstName;
            }
            if (!string.IsNullOrEmpty(loginRequestDTO.LastName))
            {
                user.LastName = loginRequestDTO.LastName;
            }

            if (loginRequestDTO.BirthDate.HasValue)
            {
                user.Birthday = loginRequestDTO.BirthDate.Value;
                user.Age = Common.CalculateAge(loginRequestDTO.BirthDate.Value);
            }

            if (!string.IsNullOrEmpty(loginRequestDTO.Gender))
            {
                user.Gender = loginRequestDTO.Gender;
            }

            if (!string.IsNullOrEmpty(loginRequestDTO.Bio))
            {
                user.Bio = loginRequestDTO.Bio;
            }

            if (!string.IsNullOrEmpty(loginRequestDTO.Location))
            {
                user.Location = loginRequestDTO.Location;
            }

            if (loginRequestDTO.Latitude.HasValue)
            {
                user.Latitude = loginRequestDTO.Latitude.Value;
            }

            if (loginRequestDTO.Longitude.HasValue)
            {
                user.Longitude = loginRequestDTO.Longitude.Value;
            }

            if (loginRequestDTO.InterestIds != null && loginRequestDTO.InterestIds.Any())
            {
                user.USERINTERESTs= new List<UserInterestDTO>();
                foreach(var interestId in loginRequestDTO.InterestIds)
                {
                    user.USERINTERESTs.Add(new UserInterestDTO
                    {
                        InterestId = interestId,
                        UserId = user.UserId
                    });
                }
            }
            user.AllowContactAccess = loginRequestDTO.AllowContactAccess ?? false;
            user.EnableNotifications = loginRequestDTO.EnableNotifications ?? true;
            var userUpdateMap = _mapper.Map<USER>(user);
            var userUpdateResult= await _userRepository.UpdateProfileDetails(userUpdateMap);
            var token = GenerateToken(user.UserId, user.PhoneNumber, user.Role, string.Empty);
            mappedUserData.LastLogin = DateTime.UtcNow;
            var loginUpdateMap = _mapper.Map<USER>(mappedUserData);
            await _userRepository.UpdateLastLogin(loginUpdateMap);
            return new UserLoginDTO
            {
                PhoneNumber = mappedUserData.PhoneNumber,
                FirstName = mappedUserData.FirstName,
                LastName = mappedUserData.LastName,
                SecretKey = token,
                UserData = user
            };
        }

        // GENERATE TOKEN FOR JWT 
        private string GenerateToken(BigInteger userId, string username, string userType, string permissions)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, userType ),
            new Claim("Permission", permissions ),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }




    }
}
