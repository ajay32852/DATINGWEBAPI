namespace DATINGWEBAPI.DTO.DTOs
{
    public class NotificationDTO
    {
        public long NotificationId { get; set; }
        public long? UserId { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Type { get; set; }
        public virtual UserDTO? USER { get; set; }

    }
}
