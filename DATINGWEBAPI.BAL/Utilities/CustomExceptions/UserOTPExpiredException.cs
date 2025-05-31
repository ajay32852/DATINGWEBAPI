namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{


    public class UserOTPExpiredException : Exception
        {
            public UserOTPExpiredException()
            {
            }



            public UserOTPExpiredException(string message)
                : base(message)
            {
            }

            public UserOTPExpiredException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

    }



