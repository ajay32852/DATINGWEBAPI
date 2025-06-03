namespace DATINGWEBAPI.BAL.Utilities.Common
{
    public enum UserType
    {
        Admin = 1,
        Owner = 2
    }
    public enum ResponseMessage
    {
        Success = 1,
        Fail = 2,
        SuccessLogin = 3,
        InvalidUser = 4,
        InvalidPassword = 5,
        ExpiredPassword = 6,
        InvalidIP = 7,
        BlockedUser = 8,
        InvalidOTP = 9,
        SuccessForgetPassword = 10,
        SuccessResetPassword = 11,
        ExpiredTicket = 12,
        UsedTicket = 13,
        InvalidTicket = 14,
        OTPExpire = 15,
        InvalidUsername = 16,
        InvalidPasswordUserType = 17,
        InvalidPasswordLength = 18,
        PasswordRuleValidation = 19,
        DataNotFound = 20,
        UserNotFound = 21,
        InvalidMobileNumber = 22,
    }

}
