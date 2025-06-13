namespace DATINGWEBAPI.DTO.DTOs
{
    public class NotificationSettingDTO
    {
        public long NotificationSettingId { get; set; }
        public long UserId { get; set; }
        public bool ReceiveMatchNotifications { get; set; }
        public bool ReceiveMessageNotifications { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual UserDTO? USER { get; set; }

    }
}
