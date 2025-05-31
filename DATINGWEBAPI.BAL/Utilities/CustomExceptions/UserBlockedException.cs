

namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class UserBlockedException : Exception
    {

        public UserBlockedException()
        {

        }
        public UserBlockedException(string message)
           : base(message)
        {

        }

        public UserBlockedException(string message, Exception inner)
            : base(message, inner)
        {

        }

    }
}
