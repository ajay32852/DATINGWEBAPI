
using DATINGWEBAPI.DTO.DTOs;
namespace DATINGWEBAPI.DTO.RequestDTO
{
    public class SwipeMatchResponseDTO
    {
        public List<UserDTO>? SwipeProfiles { get; set; }
        public List<UserDTO>? NewMatches { get; set; }

    }
}
