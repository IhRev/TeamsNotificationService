using TeamsNotificationService.Exceptions;
using TeamsNotificationService.System;

namespace TeamsNotificationService.Services.Implementations
{
	public class NotificationConfigurationProvider : IConfigurationService
	{
        private const string FILE_EXTENSION = "json";
        private const string CONFIGURATIONS_FOLDER = "NotificationServiceConfigurations";
        private readonly IJsonWrapper jsonWraapper;
        private readonly IIOWrapper iOWrapper;

        public NotificationConfigurationProvider(IJsonWrapper jsonWraapper, IIOWrapper iOWrapper)
        {
            this.jsonWraapper = jsonWraapper;
            this.iOWrapper = iOWrapper;
        }

        public async Task<T> GetConfigurationAsync<T>(string notificationServiceName)
        {
            string path = GetConfigurationPath(notificationServiceName);
            string configurationAsString = await GetConfigurationAsString(path);
            if (string.IsNullOrWhiteSpace(configurationAsString))
            {
                throw new ConfigurationNotFoundException("Notification configuration is not found");
            }
            return SerializeConfiguration<T>(configurationAsString);
        }

        private string GetConfigurationPath(string notificationServiceName) =>
            $"{iOWrapper.AppPath}{CONFIGURATIONS_FOLDER}/{notificationServiceName.ToLower()}.{FILE_EXTENSION}";

        private async Task<string> GetConfigurationAsString(string path) =>
            await iOWrapper.ReadAllTextAsync(path);

        private T SerializeConfiguration<T>(string configurationAsString) =>
            jsonWraapper.Deserialize<T>(configurationAsString);
    }
}