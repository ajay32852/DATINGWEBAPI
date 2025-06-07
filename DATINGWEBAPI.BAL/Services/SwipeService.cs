using System.Collections.Generic;
using AutoMapper;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.BAL.Utilities.CustomExceptions;
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
    public class SwipeService : ISwipeService
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SwipeService> _localizer;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISwipeRepository _swipeRepository;

        public SwipeService(
            IMapper mapper,
            IStringLocalizer<SwipeService> localizer,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ISwipeRepository swipeRepository
            )
        {
            _mapper = mapper;
            _localizer = localizer;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _swipeRepository = swipeRepository;
        }

        public async Task<SwipeMatchResponseDTO> SwipeProfile(long userId, int pageNumber, int pageSize)
        {
            // Swipeable profiles
            var swipeResult = await _swipeRepository.SwipeProfile(userId, pageNumber, pageSize);
            if (swipeResult == null || !swipeResult.Any())
            {
                throw new NoDataException(_localizer[ResponseMessage.DataNotFound.ToString()]);
            }
            // Recent matches
            var matches = await _swipeRepository.GetMatchesByUserIdAsync(userId);
            return new SwipeMatchResponseDTO
            {
                SwipeProfiles = _mapper.Map<List<UserDTO>>(swipeResult),
                NewMatches = _mapper.Map<List<UserDTO>>(matches)
            };
        }


        public async Task<SwipeDTO> SaveSwipeProfile(long userId, SwipeRequestDTO request)
        {
            if (userId == request.SwipedId)
            {
                throw new LikeUserException(_localizer[ResponseMessage.LikeUserCurrentUserNotAllowed.ToString()]);
            }

            // ✅ Validate target user
            var targetUser = await _swipeRepository.getExistsLikeUserProfileById(request.SwipedId);
            if (targetUser is null)
            {
                throw new NoDataException(_localizer[ResponseMessage.DataNotFound.ToString()]);
            }

            // ✅ Check if swipe already exists
            var existingSwipe = await _swipeRepository.getExistsLikeProfileById(request.SwipedId,userId);
            if (existingSwipe is not null && existingSwipe.LIKED && request.Liked)
            {
                throw new DuplicateValueException(_localizer[ResponseMessage.AlreadyExistsData.ToString()]);
            }

            // ✅ Save new swipe
            var swipeEntity = new SWIPE
            {
                SWIPERID = userId,
                SWIPEDID = request.SwipedId,
                LIKED = request.Liked,
                TIMESTAMP = DateTime.UtcNow
            };

            var savedSwipe = await _swipeRepository.SwipeProfile(swipeEntity);
            // ✅ Check & save match if mutual like
            if (request.Liked && savedSwipe != null)
            {
                var isMatch = await _swipeRepository.FindMutualMatch(userId, request.SwipedId);

                // Optional: notify both users via SignalR here
            }
            return _mapper.Map<SwipeDTO>(savedSwipe);
        }

       
    }
}
