namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class UserSecurityQuestionNotSet : Exception
    {
        public UserSecurityQuestionNotSet()
        {

        }
        public UserSecurityQuestionNotSet(string message)
       : base(message)
        {

        }

        public UserSecurityQuestionNotSet(string message, Exception inner)
      : base(message, inner)
        {

        }

    }
}
