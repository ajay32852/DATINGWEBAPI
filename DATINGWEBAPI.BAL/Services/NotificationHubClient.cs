using DATINGWEBAPI.BAL.Hubs;
using DATINGWEBAPI.BAL.Services.IServices;
using Microsoft.AspNetCore.SignalR;

namespace DATINGWEBAPI.BAL.Services
{
    public class NotificationHubClient : INotificationHubClient
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationHubClient(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(long userId, object notification)
        {
            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", notification);
        }

    }
}
