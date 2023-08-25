using TeamsNotificationService.Exceptions;
using TeamsNotificationService.Models;
using TeamsNotificationService.System;

namespace TeamsNotificationService.Services.Implementations
{
	public class ConfigurationService : IConfigurationService
	{
        private const string FILE_EXTENSION = "json";
        private const string CONFIGURATIONS_FOLDER = "NotificationServiceConfigurations";
        private readonly IJsonWrapper jsonWraapper;
        private readonly IIOWrapper iOWrapper;

        public ConfigurationService(IJsonWrapper jsonWraapper, IIOWrapper iOWrapper)
        {
            this.jsonWraapper = jsonWraapper;
            this.iOWrapper = iOWrapper;
        }

        public async Task<NotificationConfiguration> GetConfigurationAsync(string notificationServiceName)
        {
            string path = GetConfigurationPath(notificationServiceName);
            string configurationAsString = await GetConfigurationAsString(path);
            if (string.IsNullOrWhiteSpace(configurationAsString))
            {
                throw new ConfigurationNotFoundException("Notification configuration is not found");
            }
            return SerializeConfiguration(configurationAsString);
        }

        private string GetConfigurationPath(string notificationServiceName) =>
            $"{iOWrapper.AppPath}{CONFIGURATIONS_FOLDER}/{notificationServiceName.ToLower()}.{FILE_EXTENSION}";

        private async Task<string> GetConfigurationAsString(string path) =>
            await iOWrapper.ReadAllTextAsync(path);

        private NotificationConfiguration SerializeConfiguration(string configurationAsString) =>
            jsonWraapper.Deserialize<NotificationConfiguration>(configurationAsString);
    }
}