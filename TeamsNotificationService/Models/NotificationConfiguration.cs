namespace TeamsNotificationService.Models
{
	public class NotificationConfiguration
	{
		public string RecipientName { get; set; } = string.Empty;

        public string WebhookUrl { get; set; } = string.Empty;
    }
}