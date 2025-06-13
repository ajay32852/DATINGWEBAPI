using Microsoft.AspNetCore.SignalR;
namespace DATINGWEBAPI.BAL.Hubs
{
    public class NotificationHub : Hub
    {
        // This hub is used to manage user connections and group memberships based on user identifiers.
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
        }

        // <summary> OnDisconnectedAsync method is called when a user disconnects from the hub.
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }
        }

    }
}
