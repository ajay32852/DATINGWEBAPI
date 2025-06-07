using System.ComponentModel.DataAnnotations;

namespace DATINGWEBAPI.DTO.RequestDTO
{
    public class SwipeRequestDTO
    {
        [Required(ErrorMessage = "SwipedId is required")]
        [Range(1, long.MaxValue, ErrorMessage = "SwipedId must be a positive number")]
        public long SwipedId { get; set; }


        [Required(ErrorMessage = "Liked field is required")]
        public bool Liked { get; set; }
    }
}
