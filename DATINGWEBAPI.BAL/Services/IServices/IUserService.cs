using DATINGWEBAPI.DTO.DTOs;

namespace DATINGWEBAPI.BAL.Services.IServices
{
    public interface IUserService
    {
        Task<UserDTO> getUserDetailByUserId(long userId);
    }
}
