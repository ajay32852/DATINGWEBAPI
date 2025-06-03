using DATINGWEBAPI.DAL.DataContext;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DATINGWEBAPI.DAL.Repositories
{

    public class VerificationCodeRepository(DatingAPPContext datingAPPContext, IHttpContextAccessor httpContextAccessor)
    : IVerificationCodeRepository
    {
        public async Task<VERIFICATIONCODE> saveVerificationCode(VERIFICATIONCODE vERIFICATIONCODE)
        {
            await using var transaction = await datingAPPContext.Database.BeginTransactionAsync();
            try
            {
                var vERIFICATIONCODEObj = new VERIFICATIONCODE
                {
                    USERID = vERIFICATIONCODE.USERID,
                    PHONENUMBER = vERIFICATIONCODE.PHONENUMBER,
                    CODE = vERIFICATIONCODE.CODE,
                    EXPIRESAT = vERIFICATIONCODE.EXPIRESAT,
                    ISUSED = vERIFICATIONCODE.ISUSED,
                    CREATEDAT = DateTime.UtcNow
                };
                datingAPPContext.VERIFICATIONCODEs.Add(vERIFICATIONCODEObj);
                await datingAPPContext.SaveChangesAsync();
                return vERIFICATIONCODEObj;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error saving verification code", ex);
            }
            finally
            {
                await transaction.CommitAsync();

            }
        }
        /// <summary>
        ///  get leatest verification code by phone number  
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<VERIFICATIONCODE> getVerificationCodeByPhoneNumber(string phoneNumber)
        {
            return await datingAPPContext.VERIFICATIONCODEs.Where(x => x.PHONENUMBER == phoneNumber).OrderByDescending(x => x.CREATEDAT).FirstOrDefaultAsync();
        }
        public async Task<VERIFICATIONCODE> updateVerificationCode(VERIFICATIONCODE vERIFICATIONCODE)
        {
            await using var transaction = await datingAPPContext.Database.BeginTransactionAsync();
            try
            {
                var userVerification = await datingAPPContext.VERIFICATIONCODEs.FirstOrDefaultAsync(x => x.USERID == vERIFICATIONCODE.USERID);
                if (userVerification != null)
                {
                    userVerification.ISUSED = vERIFICATIONCODE.ISUSED;
                    userVerification.USERID = vERIFICATIONCODE.USERID;
                    datingAPPContext.VERIFICATIONCODEs.Update(userVerification);
                    await datingAPPContext.SaveChangesAsync();
                }
                return userVerification;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error", ex);
            }
            finally
            {
                await transaction.CommitAsync();

            }
        }

    }
}

