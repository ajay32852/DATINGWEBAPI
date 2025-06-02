
using AutoMapper;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using DATINGWEBAPI.DTO.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
namespace DATINGWEBAPI.BAL.Services
{
    public class UserLike : IUserLike
    {

        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UserLike> _localizer;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserLike(
            IMapper mapper,
            IStringLocalizer<UserLike> localizer,
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

        public Task<List<long>> GetLikedByProfilesAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<long>> GetLikedProfilesAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsProfileLikedAsync(ProfileLikeRequest profileLikeRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LikeProfileAsync(ProfileLikeRequest profileLikeRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnlikeProfileAsync(ProfileLikeRequest profileLikeRequest)
        {
            throw new NotImplementedException();
        }
    }
}
