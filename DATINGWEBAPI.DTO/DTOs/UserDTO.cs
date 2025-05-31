using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DATINGWEBAPI.DTO.DTOs
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string AuthProvider { get; set; }

        public string ProviderId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public int? Age { get; set; }

        public string Bio { get; set; }

        public string Location { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Role { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime LastLogin { get; set; }

        public bool IsBlocked { get; set; }


    }
}
