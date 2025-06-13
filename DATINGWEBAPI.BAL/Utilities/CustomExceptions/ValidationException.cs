namespace DATINGWEBAPI.BAL.Utilities.CustomExceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
