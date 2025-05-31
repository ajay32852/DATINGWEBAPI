using System.Net;

namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class UserPasswordIncorrectException : Exception
    {
        public int LoginCount { get; set; }

        public UserPasswordIncorrectException()
        {

        }

        public UserPasswordIncorrectException(string message, int logincount)
       : base(message)
        {
          this.LoginCount = logincount;
        }

        public UserPasswordIncorrectException(string message, Exception inner)
     : base(message, inner)
        {
        }


    }
}
