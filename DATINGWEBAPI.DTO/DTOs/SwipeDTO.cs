using System;
using System.ComponentModel.DataAnnotations;

namespace DATINGWEBAPI.DTO.DTOs
{
    public class SwipeDTO
    {
        public long SwiperId { get; set; }
        public long SwipedId { get; set; }
        public bool Liked { get; set; }
        public DateTime Timestamp { get; set; }

        public UserDTO? Swiper { get; set; }
        public UserDTO? Swiped { get; set; }
    }
}
