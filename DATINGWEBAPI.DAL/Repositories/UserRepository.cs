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
        public async Task<USER> AddNewUser(USER uSER)
        {

            await using var transaction = await datingAPPContext.Database.BeginTransactionAsync();
            try
            {
                datingAPPContext.USERs.Add(uSER);
                await datingAPPContext.SaveChangesAsync();
                return uSER;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error saving", ex);
            }
            finally
            {
                await transaction.CommitAsync();

            }
        }
        public async Task<USER?> GetUserByMobileAsync(string mobileNo)
        {
            try
            {
                return await datingAPPContext.USERs.Include(x=>x.USERINTERESTs).Where(u => u.PHONENUMBER.Trim() == mobileNo.Trim()).FirstOrDefaultAsync();
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

        public async Task<USER> UpdateProfileDetails(USER userUpdateMap)
        {
            await using var transaction = await datingAPPContext.Database.BeginTransactionAsync();
            try
            {
                var user = await datingAPPContext.USERs
                    .Include(u => u.USERINTERESTs).Where(u => u.USERID == userUpdateMap.USERID).FirstOrDefaultAsync();
               
                user.FIRSTNAME = userUpdateMap.FIRSTNAME;
                user.LASTNAME = userUpdateMap.LASTNAME;
                user.BIO = userUpdateMap.BIO;
                user.LOCATION = userUpdateMap.LOCATION;
                user.GENDER = userUpdateMap.GENDER;
                user.BIRTHDAY = userUpdateMap.BIRTHDAY;
                user.AGE = userUpdateMap.AGE;
                user.PROFILEIMAGEURL = userUpdateMap.PROFILEIMAGEURL;
                user.LATITUDE = userUpdateMap.LATITUDE;
                user.LONGITUDE = userUpdateMap.LONGITUDE;
                user.ISPROFILECOMPLETE = userUpdateMap.ISPROFILECOMPLETE;
                user.UPDATEDAT = DateTime.UtcNow;
                user.ISBLOCKED = userUpdateMap.ISBLOCKED;
                user.ALLOWCONTACTACCESS = userUpdateMap.ALLOWCONTACTACCESS;
                user.ENABLENOTIFICATIONS = userUpdateMap.ENABLENOTIFICATIONS;
                if (userUpdateMap.USERINTERESTs != null && userUpdateMap.USERINTERESTs.Any())
                {
                    // Remove existing interests
                    datingAPPContext.USERINTERESTs.RemoveRange(user.USERINTERESTs);

                    // Add new interests
                    var newInterests = userUpdateMap.USERINTERESTs
                        .Select(i => new USERINTEREST
                        {
                            USERID = user.USERID,
                            INTERESTID = i.INTERESTID
                        }).ToList();

                    await datingAPPContext.USERINTERESTs.AddRangeAsync(newInterests);
                }
                await datingAPPContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return user;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error", ex);
            }
        }
        public async Task<USER> GetUserByUserId(long userID)
        {
            var user = await datingAPPContext.USERs
                 .Include(u => u.USERINTERESTs)
                 .Include(x=>x.USER_MEDIa)
                 .Where(u => u.USERID == userID)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();
            return user;
        }




    }
}
