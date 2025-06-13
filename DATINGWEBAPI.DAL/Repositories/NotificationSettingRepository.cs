using DATINGWEBAPI.DAL.DataContext;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
namespace DATINGWEBAPI.DAL.Repositories
{
    public class NotificationSettingRepository(DatingAPPContext datingAPPContext, IHttpContextAccessor httpContextAccessor)
   : INotificationSettingRepository
    {
        public async Task<bool> CreateDefaultNotificationSettingAsync(NOTIFICATIONSETTING nOTIFICATIONSETTING)
        {
            await using var transaction = await datingAPPContext.Database.BeginTransactionAsync();
            try
            {
                await datingAPPContext.NOTIFICATIONSETTINGs.AddAsync(nOTIFICATIONSETTING);
                await datingAPPContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
