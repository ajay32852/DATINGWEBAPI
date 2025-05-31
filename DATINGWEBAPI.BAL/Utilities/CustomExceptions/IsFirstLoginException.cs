namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class IsFirstLoginException:Exception
    {
        public int StatusCode { get; }
        public IsFirstLoginException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
        public IsFirstLoginException(string message)
           : base(message)
        {

        }

        public IsFirstLoginException(string message, Exception inner)
            : base(message, inner)
        {

        }

    }
}
