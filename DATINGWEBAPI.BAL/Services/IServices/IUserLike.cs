using DATINGWEBAPI.DTO.RequestDTO;

namespace DATINGWEBAPI.BAL.Services.IServices
{
    public interface IUserLike
    {
        Task<bool> LikeProfileAsync(ProfileLikeRequest profileLikeRequest);
        Task<bool> UnlikeProfileAsync(ProfileLikeRequest profileLikeRequest);
        Task<List<long>> GetLikedProfilesAsync(long userId);
        Task<List<long>> GetLikedByProfilesAsync(long userId);
        Task<bool> IsProfileLikedAsync(ProfileLikeRequest profileLikeRequest); 
    }
}
