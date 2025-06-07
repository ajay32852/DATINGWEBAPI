
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

namespace DATINGWEBAPI.BAL.Services
{
    public class UserService : IUserService
    {

        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UserService> _localizer;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserService(
            IMapper mapper,
            IStringLocalizer<UserService> localizer,
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

        public async Task<UserDTO> getUserDetailByUserId(long userId)
        {
            var userResponse = await _userRepository.GetUserByUserId(userId);
            var user = _mapper.Map<UserDTO>(userResponse);
            return user;

        }
        public async Task<UserDTO> UpdateUserProfileAsync(long userId, UpdateUserProfileRequestDTO request)
        {
            var existingUserResponse = await _userRepository.GetUserByUserId(userId);
            var existingUser= _mapper.Map<UserDTO>(existingUserResponse);
            if (existingUser == null)
            {
                throw new UserNotFoundException(_localizer[ResponseMessage.InvalidUser.ToString()]);
            }
            if (request.FirstName != null)
            {
                existingUser.FirstName = request.FirstName;
            } 
            if (request.LastName != null)
            {
                existingUser.LastName = request.LastName;
            }
            if (request.BirthDate.HasValue)
            {
                existingUser.Birthday = request.BirthDate.Value;
                existingUser.Age = Common.CalculateAge(request.BirthDate.Value);
            } 
            if (request.Gender != null)
            {
                existingUser.Gender = request.Gender;
            }
            if (request.Bio != null)
            {
                existingUser.Bio = request.Bio;
            }
            if (request.Location != null)
            {
                existingUser.Location = request.Location;
            } 
            if (request.Latitude.HasValue)
            {
                existingUser.Latitude = request.Latitude.Value;
            }
            if (request.Longitude.HasValue)
            {
                existingUser.Longitude = request.Longitude.Value;
            }            if (request.AllowContactAccess.HasValue)
            {
                existingUser.AllowContactAccess = request.AllowContactAccess.Value;
            }
            if (request.EnableNotifications.HasValue)
            {
                existingUser.EnableNotifications = request.EnableNotifications.Value;
            }
            var updateUserProfile = _mapper.Map<USER>(existingUser);
            var userUpdateResponse =await  _userRepository.UpdateProfileDetails(updateUserProfile);
            var userResponse = _mapper.Map<UserDTO>(userUpdateResponse);
            return userResponse;
        }

      
    }
}
