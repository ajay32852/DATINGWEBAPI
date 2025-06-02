
using AutoMapper;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using DATINGWEBAPI.DTO.DTOs;
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
    }
}
