namespace TeamsNotificationService.Services
{
	public interface IConfigurationService
	{
		Task<T> GetConfigurationAsync<T>(string notificationServiceName);
	}
}