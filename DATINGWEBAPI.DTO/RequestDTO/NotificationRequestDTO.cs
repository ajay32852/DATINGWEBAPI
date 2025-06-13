using System.ComponentModel.DataAnnotations;

namespace DATINGWEBAPI.DTO.RequestDTO
{
    public class NotificationRequestDTO
    {

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }


        [StringLength(50)]
        public string? Type { get; set; }
    }
}
