using AdaptiveCards;

namespace TeamsNotificationService.DTOs
{
	public class NotificationDTO
	{
        public string SourceSystem { get; set; } = null!;

        public string Recipient { get; set; } = null!;

        public AdaptiveCard Card { get; set; } = null!;
    }
}