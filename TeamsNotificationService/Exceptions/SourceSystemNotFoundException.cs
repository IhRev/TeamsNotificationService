namespace TeamsNotificationService.Exceptions
{
	public class SourceNotFoundException : Exception
	{
		public SourceNotFoundException(string message) : base(message)
		{
		}
	}
}