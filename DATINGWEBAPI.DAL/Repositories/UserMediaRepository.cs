using DATINGWEBAPI.DAL.DataContext;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;

namespace DATINGWEBAPI.DAL.Repositories
{
    public class UserMediaRepository(DatingAPPContext datingAPPContext, IHttpContextAccessor httpContextAccessor)
: IUserMediaRepository
    {

        public async Task<USER_MEDIum> AddMediaImages(USER_MEDIum uSER_MEDIum)
        {
            await using var transaction = await datingAPPContext.Database.BeginTransactionAsync();
            try
            {
                datingAPPContext.USER_MEDIAs.Add(uSER_MEDIum);
                await datingAPPContext.SaveChangesAsync();
                return uSER_MEDIum;
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
        public async Task<USER_STORy> AddStory(USER_STORy uSER_STORy)
        {
            await using var transaction = await datingAPPContext.Database.BeginTransactionAsync();
            try
            {
                datingAPPContext.USER_STORIEs.Add(uSER_STORy);
                await datingAPPContext.SaveChangesAsync();
                return uSER_STORy;
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




    }
   
}
