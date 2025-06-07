using System.ComponentModel.DataAnnotations;

namespace DATINGWEBAPI.DTO.RequestDTO
{
    public class UpdateUserProfileRequestDTO
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be Male, Female, or Other.")]
        public string? Gender { get; set; }

        public List<int>? InterestIds { get; set; }

        public bool? AllowContactAccess { get; set; } = false;

        public bool? EnableNotifications { get; set; } = true;

        [MaxLength(500)]
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

    }

}
