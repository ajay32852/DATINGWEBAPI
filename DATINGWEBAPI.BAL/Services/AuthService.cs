using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.BLL.Utilities.CustomExceptions;
using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using DATINGWEBAPI.DAL.Entities;
using System.Numerics;

namespace DATINGWEBAPI.BAL.Services
{
    public class AuthService : IAuthService
    {

        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AuthService> _localizer;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public AuthService(
            IMapper mapper,
            IStringLocalizer<AuthService> localizer,
            IConfiguration configuration,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _mapper = mapper;
            _localizer = localizer;
            _configuration = configuration;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<UserLoginDTO?> Login(LoginRequestDTO userLoginDto)
        {
            var user = await _userRepository.GetUserByMobileAsync(userLoginDto.MobileNumber.ToString());
            if (user == null)
            {
                throw new UserNotFoundException(_localizer[ResponseMessage.InvalidUser.ToString()]);
            }
            var mappedUserData = _mapper.Map<UserDTO>(user);
            if (mappedUserData.IsBlocked) // Adjust according to your DTO property
            {
                throw new UserBlockedException(_localizer[ResponseMessage.BlockedUser.ToString()]);
            }
            var token = GenerateToken(user.USERID, user.PHONENUMBER, user.ROLE, string.Empty);
            mappedUserData.LastLogin = DateTime.UtcNow;
            var loginUpdateMap = _mapper.Map<USER>(mappedUserData);
            await _userRepository.UpdateLastLogin(loginUpdateMap);
            var otp = new Random().Next(1000, 9999);
            // 7. Save OTP in database or cache (not shown here)
           // await _otpService.SaveOtpAsync(user.MobileNumber, otp.ToString());

            // 8. Send OTP via SMS (assume your SMS service handles formatting/delivery)
            //await _smsService.SendOtpAsync(user.MobileNumber, otp.ToString());
            return new UserLoginDTO
            {
                PhoneNumber = mappedUserData.PhoneNumber,
                FirstName = mappedUserData.FirstName,
                LastName = mappedUserData.LastName,
                SecretKey = token,
                OTP = otp,
                UserData = mappedUserData
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
