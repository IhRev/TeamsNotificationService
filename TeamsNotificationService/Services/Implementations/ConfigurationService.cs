using TeamsNotificationService.Exceptions;
using TeamsNotificationService.Models;
using TeamsNotificationService.System;

namespace TeamsNotificationService.Services.Implementations
{
	public class ConfigurationService : IConfigurationService
    {
        private readonly IJsonWrapper jsonWraapper;
        private readonly IIOWrapper iOWrapper;

        public ConfigurationService(IJsonWrapper jsonWraapper, IIOWrapper iOWrapper)
		{
            this.jsonWraapper = jsonWraapper;
            this.iOWrapper = iOWrapper;
        }

        public NotificationConfiguration GetConfiguration(string notificationServiceName)
        {
            string path = GetConfigurationPath(notificationServiceName);
            string configurationAsString = GetConfigurationAsString(path);
            if (string.IsNullOrWhiteSpace(configurationAsString))
            {
                throw new ConfigurationNotFoundException("Notification configuration is not found");
            }
            return SerializeConfiguration(configurationAsString);
        }

        private string GetConfigurationPath(string notificationServiceName)
        {
            return "";
        }

        private string GetConfigurationAsString(string path)
        {
            return "";
        }

        private NotificationConfiguration SerializeConfiguration(string configurationAsString) =>
            jsonWraapper.Deserialize<NotificationConfiguration>(configurationAsString);
	}
}