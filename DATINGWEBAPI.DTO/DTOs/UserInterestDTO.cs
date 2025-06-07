

namespace DATINGWEBAPI.DTO.DTOs
{
    public class UserInterestDTO
    {
        public long UserInterestId { get; set; }
        public long UserId { get; set; }
        public long InterestId { get; set; }
        public virtual InterestDTO INTEREST { get; set; }

        public virtual UserDTO USER { get; set; }
    }
}
