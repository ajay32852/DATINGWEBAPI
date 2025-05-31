using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATINGWEBAPI.DTO.DTOs
{
    public class UserLoginDTO
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecretKey { get; set; } // JWT
        public int OTP { get; set; } 
        public UserDTO UserData { get; set; }
    }

}
