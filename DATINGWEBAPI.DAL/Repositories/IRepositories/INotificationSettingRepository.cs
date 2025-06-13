using DATINGWEBAPI.DAL.Entities;

namespace DATINGWEBAPI.DAL.Repositories.IRepositories
{
    public interface INotificationSettingRepository
    {
        Task<bool> CreateDefaultNotificationSettingAsync(NOTIFICATIONSETTING nOTIFICATIONSETTING);
    }
}
