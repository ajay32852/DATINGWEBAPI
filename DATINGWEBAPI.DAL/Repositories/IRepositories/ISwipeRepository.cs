using Azure.Core;
using DATINGWEBAPI.DAL.Entities;

namespace DATINGWEBAPI.DAL.Repositories.IRepositories
{
    public interface ISwipeRepository
    {
        Task<SWIPE> SwipeProfile(SWIPE swipeEntity);
        Task<List<USER>> SwipeProfile(long userId, int pageNumber, int pageSize);
        Task<USER> getExistsLikeUserProfileById(long SwipedId);
        Task<SWIPE> getExistsLikeProfileById(long SwipedId,long userId);
        Task<List<USER>> GetMatchesFromMatchesTableAsync(long userId);
        Task<List<USER>> GetMatchesByUserIdAsync(long userId);
        Task<bool> FindMutualMatch(long SWIPERID,long SWIPEDID);

    }
}
