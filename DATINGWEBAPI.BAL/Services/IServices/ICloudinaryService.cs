using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;

namespace DATINGWEBAPI.BAL.Services.IServices
{
    public interface ICloudinaryService
    {
        Task<UserMediaDTO> AddMediaImages(long userId, MediaUploadRequest mediaUploadRequest);
        Task<UserStoryDTO> AddStory(long userId, CreateStoryRequestDTO request);
        Task<List<UserMediaDTO>> getMediaImages(long userId);
        Task<bool> DeleteMediaImage(long userId,string mediaId);
    }

}
