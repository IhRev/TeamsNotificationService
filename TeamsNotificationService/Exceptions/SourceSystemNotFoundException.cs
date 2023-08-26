namespace TeamsNotificationService.Exceptions
{
	public class SourceSystemNotFoundException : Exception
	{
		public SourceSystemNotFoundException(string message) : base(message)
		{
		}
	}
}