using System.Text;
using TeamsNotificationService.Core;
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
            NotificationConfiguration configuration = await configurationService
               .GetConfigurationAsync<NotificationConfiguration>(notificationData.SourceSystem);
            StringContent content = GetStringContent(notificationData);
            HttpResponseMessage respone = await httpWrapper.PostAsync(configuration.WebhookUrl, content);
            respone.EnsureSuccessStatusCode();
        }

        private StringContent GetStringContent(Notification notificationData)
        {
            var cardContent = new StringContent(notificationData.JsonContent, Encoding.UTF8, "application/json");
            return cardContent;
        }
    }
}