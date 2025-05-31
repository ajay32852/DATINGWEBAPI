
namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class UserInActiveResetQuestionException : Exception
    {

        public UserInActiveResetQuestionException()
        {

        }
        public UserInActiveResetQuestionException(string message)
           : base(message)
        {

        }

        public UserInActiveResetQuestionException(string message, Exception inner)
            : base(message, inner)
        {

        }

    }
}
