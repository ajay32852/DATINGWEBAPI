using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;
namespace DATINGWEBAPI.BAL.Services.IServices
{
    public interface IAuthService
    {
        Task<OTPMobileNoResponse?> RequestOTP(RequestOTPDTO requestOTPDTO);
        Task<UserLoginDTO?> VerifyOTP(LoginRequestDTO loginRequestDTO);
        
    }
}
