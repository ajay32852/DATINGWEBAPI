namespace DATINGWEBAPI.DTO.DTOs
{
    public class VerificationCodeDTO
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? UserId { get; set; }
        public virtual UserDTO? User { get; set; }
    }

}