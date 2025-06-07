using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;

namespace DATINGWEBAPI.BAL.Services.IServices
{
    public interface IUserService
    {
        Task<UserDTO> getUserDetailByUserId(long userId);
        Task<UserDTO> UpdateUserProfileAsync(long userId, UpdateUserProfileRequestDTO request);

    }
}
