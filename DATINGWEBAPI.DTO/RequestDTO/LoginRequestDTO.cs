
using Microsoft.AspNetCore.Http;

namespace DATINGWEBAPI.DTO.RequestDTO
{
    public class LoginRequestDTO
    {
        public string MobileNumber { get; set; }
        public string? OTP { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public List<int>? InterestIds { get; set; }
        public bool? AllowContactAccess { get; set; }=false;

        public bool? EnableNotifications { get; set; }= true;
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public IFormFile? ProfileImage { get; set; }

    }
}
