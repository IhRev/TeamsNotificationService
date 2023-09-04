using System.Text;
using TeamsNotificationService.Core;
using TeamsNotificationService.Exceptions;
using TeamsNotificationService.Models;
using TeamsNotificationService.System;

namespace TeamsNotificationService.Services.Implementations
{
	public class TeamsNotificationSender : INotificationSender
	{
        private readonly IConfigurationService configurationService;
        private readonly IHttpWrapper httpWrapper;

        public TeamsNotificationSender(IConfigurationService configurationService, IHttpWrapper httpWrapper)
		{
            this.configurationService = configurationService;
            this.httpWrapper = httpWrapper;
        }

        public string SourceSystem => "Teams";

        public async Task SendNotification(Notification notificationData)
        {
            NotificationConfiguration configuration = await GetConfiguration(notificationData);
            StringContent content = GetStringContent(notificationData);
            HttpResponseMessage respone = await httpWrapper.PostAsync(configuration.WebhookUrl, content);
            respone.EnsureSuccessStatusCode();
        }

        private async Task<NotificationConfiguration> GetConfiguration(Notification notificationData)
        {
            IEnumerable<NotificationConfiguration> configurations = await configurationService
             .GetConfigurationAsync<IEnumerable<NotificationConfiguration>>(notificationData.SourceSystem);
            NotificationConfiguration? configuration = configurations
                .FirstOrDefault(c => c.RecipientName == notificationData.Recipient);
            return configuration ?? throw new SourceNotFoundException("Recipient not fount");
        }

        private StringContent GetStringContent(Notification notificationData)
        {
            var cardContent = new StringContent(notificationData.Card.ToJson(), Encoding.UTF8, "application/json");
            return cardContent;
        }
    }
}