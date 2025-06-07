namespace DATINGWEBAPI.DTO.DTOs
{
    public class InterestDTO
    {
        public long INTERESTID { get; set; }
        public string NAME { get; set; }
        public string ICONURL { get; set; }
        public virtual ICollection<UserInterestDTO> USERINTERESTs { get; set; }
    }
}
