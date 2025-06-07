namespace DATINGWEBAPI.BAL.Utilities.CustomExceptions
{
    public class LikeUserException : Exception
    {
        public LikeUserException()
        {

        }


        public LikeUserException(string message)
            : base(message)
        {

        }

        public LikeUserException(string message, Exception inner)
          : base(message, inner)
        {

        }
    }
}
