using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;

namespace DATINGWEBAPI.BAL.Services.IServices
{
    public interface IAuthService
    {
        Task<UserLoginDTO?> Login(LoginRequestDTO userLoginRequestDto);
    }
}
