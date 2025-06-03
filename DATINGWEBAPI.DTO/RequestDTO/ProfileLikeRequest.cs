namespace DATINGWEBAPI.DTO.RequestDTO
{
    public class ProfileLikeRequest
    {
        public long? FromUserId { get; set; }
        public long ToUserId { get; set; }
    }
}
