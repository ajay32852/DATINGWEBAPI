namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
	public class TbsException : Exception
	{
		public int ErrorCode { get; set; }

		// Default constructor
		public TbsException() : base()
		{
		}
		// Constructor that accepts only a message
		public TbsException(string message) : base(message)
		{
		}
		// Constructor that accepts a message and an inner exception (for chaining exceptions)
		public TbsException(string message, Exception inner) : base(message, inner)
		{
		}
		// Constructor that accepts a message and an error code
		public TbsException(string message, int errorCode) : base(message)
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
