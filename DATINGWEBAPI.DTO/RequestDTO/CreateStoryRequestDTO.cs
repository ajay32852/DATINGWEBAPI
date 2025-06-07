using Microsoft.AspNetCore.Http;

namespace DATINGWEBAPI.DTO.RequestDTO
{
    public class CreateStoryRequestDTO
    {
        public string? Caption { get; set; }
        public IFormFile? MediaFile { get; set; }  // Image or video
    }
}
