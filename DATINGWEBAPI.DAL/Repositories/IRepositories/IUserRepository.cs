using DATINGWEBAPI.DAL.Entities;

namespace DATINGWEBAPI.DAL.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<USER> GetUserByMobileAsync(string mobileNo);
        Task<USER> UpdateLastLogin(USER loginUpdateMap);
        Task<USER> AddNewUser(USER uSER);
        Task<USER> UpdateProfileDetails(USER userUpdateMap);
        Task<USER> GetUserByUserId(long userID);
    }
}
