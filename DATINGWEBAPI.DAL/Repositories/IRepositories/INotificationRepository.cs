using DATINGWEBAPI.DAL.Entities;

namespace DATINGWEBAPI.DAL.Repositories.IRepositories
{
    public interface INotificationRepository
    {
        Task<List<NOTIFICATION>> GetUserNotificationsAsync(long userId);
        Task<NOTIFICATION> CreateNotificationAsync(long userId, NOTIFICATION notification);
        Task<bool> MarkAsReadAsync(long userId, long notificationId);

    }
}
