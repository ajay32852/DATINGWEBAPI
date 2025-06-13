using DATINGWEBAPI.DAL.DataContext;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DATINGWEBAPI.DAL.Repositories
{
    public class NotificationRepository(DatingAPPContext datingAPPContext, IHttpContextAccessor httpContextAccessor) : INotificationRepository
    {
        public async Task<NOTIFICATION> CreateNotificationAsync(long userId, NOTIFICATION notification)
        {
            await using var transaction = await datingAPPContext.Database.BeginTransactionAsync();
            try
            {
                notification.USERID = userId;
                notification.CREATEDAT = DateTime.UtcNow;
                notification.ISREAD = false;
                await datingAPPContext.NOTIFICATIONs.AddAsync(notification);
                await datingAPPContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return notification;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<NOTIFICATION>> GetUserNotificationsAsync(long userId)
        {
            return await datingAPPContext.NOTIFICATIONs
                .Include(x=>x.USER)
                .Where(n => n.USERID == userId)
                .OrderByDescending(n => n.CREATEDAT)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> MarkAsReadAsync(long userId, long notificationId)
        {
            await using var transaction = await datingAPPContext.Database.BeginTransactionAsync();

            try
            {
                var notification = await datingAPPContext.NOTIFICATIONs
                    .FirstOrDefaultAsync(n => n.NOTIFICATIONID == notificationId && n.USERID == userId);
                if (notification == null)
                    return false;
                notification.ISREAD = true;
                datingAPPContext.NOTIFICATIONs.Update(notification);
                await datingAPPContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
