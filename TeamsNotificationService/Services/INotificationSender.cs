using TeamsNotificationService.Core;

namespace TeamsNotificationService.Services
{
	public interface INotificationSender
	{
		string SourceSystem { get; }

		Task SendNotification(Notification notificationData);
	}
}