using AdaptiveCards;

namespace TeamsNotificationService.Core
{
    public class Notification
    {
        public string NotificationId { get; set; } = Guid.NewGuid().ToString();

        public string SourceSystem { get; set; } = null!;

        public string Recipient { get; set; } = null!;

        public AdaptiveCard Card { get; set; } = null!;
    }
}