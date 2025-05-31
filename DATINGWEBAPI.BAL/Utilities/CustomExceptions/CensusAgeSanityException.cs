namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
	public class CensusAgeSanityException : Exception
	{
		// Property to store error code
		public int ErrorCode { get; set; }

		// Default constructor
		public CensusAgeSanityException() : base()
		{
		}
		// Constructor that accepts only a message
		public CensusAgeSanityException(string message) : base(message)
		{
		}
		// Constructor that accepts a message and an inner exception (for chaining exceptions)
		public CensusAgeSanityException(string message, Exception inner) : base(message, inner)
		{
		}
		// Constructor that accepts a message and an error code
		public CensusAgeSanityException(string message, int errorCode) : base(message)
		{
			ErrorCode = errorCode;
		}

		// Method to add custom data to the exception
		public void AddExceptionData(string key, object value)
		{
			Data.Add(key, value);
		}


	}
}
