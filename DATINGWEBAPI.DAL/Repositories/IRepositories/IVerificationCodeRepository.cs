using DATINGWEBAPI.DAL.Entities;

namespace DATINGWEBAPI.DAL.Repositories.IRepositories
{
    public interface IVerificationCodeRepository
    {
        Task<VERIFICATIONCODE> saveVerificationCode(VERIFICATIONCODE vERIFICATIONCODE);
        Task<VERIFICATIONCODE> updateVerificationCode(VERIFICATIONCODE vERIFICATIONCODE);
        Task<VERIFICATIONCODE> getVerificationCodeByPhoneNumber(string phoneNumber);
       
    }
}
