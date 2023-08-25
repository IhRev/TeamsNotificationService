namespace TeamsNotificationService.DTOs
{
	public class NotificationDTO
	{
        public string SourceSystem { get; set; } = null!;
		public string Sender { get; set; } = null!;
        public string Recipient { get; set; } = null!;
		public string Content { get; set; } = null!;
	}
}