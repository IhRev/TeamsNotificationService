using TeamsNotificationService.Models;

namespace TeamsNotificationService.Services
{
	public interface IConfigurationService
	{
		Task<NotificationConfiguration> GetConfigurationAsync(string notificationServiceName);
	}
}