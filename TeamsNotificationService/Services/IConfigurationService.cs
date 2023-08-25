using TeamsNotificationService.Models;

namespace TeamsNotificationService.Services
{
	public interface IConfigurationService
	{
		NotificationConfiguration GetConfiguration(string notificationServiceName);
	}
}