using DATINGWEBAPI.DAL.DataContext;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using DATINGWEBAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace DATINGWEBAPI.DAL.Repositories
{
    public class UserRepository(DatingAPPContext datingAPPContext, IHttpContextAccessor httpContextAccessor)
    : IUserRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        public async Task<USER?> GetUserByMobileAsync(string mobileNo)
        {
            try
            {
                return await datingAPPContext.USERs.FirstOrDefaultAsync(u => u.PHONENUMBER == mobileNo);
            }
            catch (Exception ex)
            {
                return null;
            }
          
        }
        public async Task<USER> UpdateLastLogin(USER loginUpdateMap)
        {
            var user = await datingAPPContext.USERs.FirstOrDefaultAsync(x => x.USERID == loginUpdateMap.USERID);
            if (user != null)
            {
                user.LASTLOGIN = loginUpdateMap.LASTLOGIN;
                datingAPPContext.USERs.Update(user);
                await datingAPPContext.SaveChangesAsync();
            }
            return user;
        }


    }
}
