using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DATINGWEBAPI.DTO.RequestDTO
{
    public class MediaUploadRequest
    {
        [Required(ErrorMessage = "Media file is required.")]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Media type is required.")]
        public string MediaType { get; set; } // e.g., "image", "video" if extended
    }
}
