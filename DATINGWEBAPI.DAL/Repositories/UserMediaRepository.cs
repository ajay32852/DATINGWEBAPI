using DATINGWEBAPI.DAL.DataContext;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<USER_MEDIum>> getMediaImages(long userId)
        {
          return await datingAPPContext.USER_MEDIAs.Where(x => x.USERID == userId).OrderByDescending(x=>x.CREATED_AT).ToListAsync();
        }
        public async Task<USER_MEDIum> mediaImagesbyMediaId(string mediaId, long userId)
        {
            return await datingAPPContext.USER_MEDIAs
                .Where(x => x.STORAGE_ID.EndsWith(mediaId))
                .Where(x => x.USERID == userId)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteMediaImage(long userId, string mediaId)
        {
            var mediaEntity = await datingAPPContext.USER_MEDIAs
                .Where(x => x.STORAGE_ID == mediaId && x.USERID == userId)
                .FirstOrDefaultAsync();
            if (mediaEntity != null)
            {
                datingAPPContext.USER_MEDIAs.Remove(mediaEntity);
                await datingAPPContext.SaveChangesAsync();
                return true;
            }
            return false;
        }



    }

}
