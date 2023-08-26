using Newtonsoft.Json.Linq;
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
            var card = new JObject
            {
                ["type"] = "message",
                ["attachments"] = new JArray
                {
                    new JObject
                    {
                        ["contentType"] = "application/vnd.microsoft.card.hero",
                        ["content"] = new JObject
                        {
                            ["title"] = notificationData.Title,
                            ["text"] = notificationData.Content,
                        }
                    }   
                }
            };

            var cardContent = new StringContent(card.ToString(), Encoding.UTF8, "application/json");
            return cardContent;
        }
    }
}