

namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class UserInActiveException : Exception
    {

        public UserInActiveException()
        {

        }
        public UserInActiveException(string message)
           : base(message)
        {

        }

        public UserInActiveException(string message, Exception inner)
            : base(message, inner)
        {

        }

    }
}
