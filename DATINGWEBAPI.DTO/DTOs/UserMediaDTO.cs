using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATINGWEBAPI.DTO.DTOs
{
    public class UserMediaDTO
    {
        public long MediaId { get; set; }

        public long UserId { get; set; }

        public string MediaUrl { get; set; }

        public string StorageId { get; set; }

        public string MediaType { get; set; }

        public bool IsProfilePic { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
