using System;
using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;
namespace DATINGWEBAPI.BAL.Services.IServices
{
    public interface ISwipeService
    {
        Task<SwipeDTO> SaveSwipeProfile(long userId, SwipeRequestDTO request);
        Task<SwipeMatchResponseDTO> SwipeProfile(long userId, int pageNumber, int pageSize);
    }
}
