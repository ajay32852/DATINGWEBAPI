using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATINGWEBAPI.DTO.RequestDTO
{
    public class LoginRequestDTO
    {
        public string MobileNumber { get; set; }
        public string Otp { get; set; }
    }
}
