namespace TeamsNotificationService.Core
{
    public class Notification
    {
        public string NotificationId { get; set; } = Guid.NewGuid().ToString();
        public string SourceSystem { get; set; } = null!;
        public string Sender { get; set; } = null!;
        public string Recipient { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}