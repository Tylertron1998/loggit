using System;

namespace Loggit.Exceptions
{
	public class InvalidTacviewDataException : Exception
	{
		public InvalidTacviewDataException(string message) : base($"Invalid Tacview Data: {message}")
		{
		}
	}
}