using System.Net;

namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class UserIPInvalidException : Exception
    {
        public UserIPInvalidException()
        {
                
        }
        public UserIPInvalidException(string message)
            : base(message)
        {

        }

        public UserIPInvalidException(string message, Exception inner)
         : base(message,inner)
        {

        }
    }
}
