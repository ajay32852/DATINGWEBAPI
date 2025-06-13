using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;

namespace DATINGWEBAPI.BAL.Services.IServices
{
    public interface INotificationService
    {
        Task<List<NotificationDTO>> GetUserNotificationsAsync(long userId);
        Task<NotificationDTO> CreateNotificationAsync(long userId, NotificationRequestDTO dto);
        Task<bool> MarkAsReadAsync(long userId,long notificationId);
    }
}
