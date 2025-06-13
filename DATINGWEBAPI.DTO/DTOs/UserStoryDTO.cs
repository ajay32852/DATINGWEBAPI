namespace DATINGWEBAPI.DTO.DTOs
{
    public class UserStoryDTO
    {
        public long StoryId { get; set; }
        public long UserId { get; set; }
        public string MediaUrl { get; set; }
        public string StorageId { get; set; }
        public string MediaType { get; set; }
        public string Caption { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual UserDTO USER { get; set; }


    }
}
