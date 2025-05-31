namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{


    public class UserOTPInvalidException : Exception
        {
            public UserOTPInvalidException()
            {
            }



            public UserOTPInvalidException(string message)
                : base(message)
            {
            }

            public UserOTPInvalidException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

    }



