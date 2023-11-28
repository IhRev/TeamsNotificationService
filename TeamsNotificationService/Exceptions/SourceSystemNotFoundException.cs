using System.Runtime.Serialization;

namespace TeamsNotificationService.Exceptions
{
	public class SourceNotFoundException : Exception
	{
        public SourceNotFoundException()
        {
        }

        public SourceNotFoundException(string message) : base(message)
		{
		}

        public SourceNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}