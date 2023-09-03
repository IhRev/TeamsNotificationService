namespace TeamsNotificationService.DTOs
{
	public class NotificationDTO
	{
        public string SourceSystem { get; set; } = null!;
        public string Recipient { get; set; } = null!;
		public string JsonContent { get; set; } = null!;
	}
}