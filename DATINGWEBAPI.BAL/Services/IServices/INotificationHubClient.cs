namespace DATINGWEBAPI.BAL.Services.IServices
{
    public interface INotificationHubClient
    {
        Task SendNotificationAsync(long userId, object notification);
    }
}
