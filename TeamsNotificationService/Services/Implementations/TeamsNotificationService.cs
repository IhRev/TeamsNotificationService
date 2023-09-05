using System.Text;
using Newtonsoft.Json.Linq;
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
            var card = GetAdaptiveCard();
            var cardContent = new StringContent(card, Encoding.UTF8, "application/json");
            return cardContent;
        }
        private string GetAdaptiveCard()
        {
            var card = new JObject
            {
                ["type"] = "message",
                ["attachments"] = new JArray
                {
                    new JObject
                    {
                        ["contentType"] = "application/vnd.microsoft.card.adaptive",
                        ["content"] = new JObject
                        {
                            ["$schema"] = "http://adaptivecards.io/schemas/adaptive-card.json",
                            ["type"] = "AdaptiveCard",
                            ["version"] = "1.2",
                            ["body"] = new JArray
                            {
                                new JObject
                                {
                                    ["type"] = "TextBlock",
                                    ["text"] = "Press the buttons to toggle the images!",
                                    ["wrap"] = true
                                },
                                new JObject
                                {
                                    ["type"] = "TextBlock",
                                    ["text"] = "Here are some images:",
                                    ["isVisible"] = false,
                                    ["id"] = "textToToggle"
                                },
                                new JObject
                                {
                                    ["type"] = "ColumnSet",
                                    ["columns"] = new JArray
                                    {
                                        new JObject
                                        {
                                            ["type"] = "Column",
                                            ["items"] = new JArray
                                            {
                                                new JObject
                                                {
                                                    ["style"] = "person",
                                                    ["type"] = "Image",
                                                    ["url"] = "https://picsum.photos/100/100?image=112",
                                                    ["isVisible"] = false,
                                                    ["id"] = "imageToToggle",
                                                    ["altText"] = "sample image 1",
                                                    ["size"] = "medium"
                                                }
                                            }
                                        },
                                        new JObject
                                        {
                                            ["type"] = "Column",
                                            ["items"] = new JArray
                                            {
                                                new JObject
                                                {
                                                    ["type"] = "Image",
                                                    ["url"] = "https://picsum.photos/100/100?image=123",
                                                    ["isVisible"] = false,
                                                    ["id"] = "imageToToggle2",
                                                    ["altText"] = "sample image 2",
                                                    ["size"] = "medium"
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            ["actions"] = new JArray
                            {
                                new JObject
                                {
                                    ["type"] = "Action.ToggleVisibility",
                                    ["title"] = "Toggle!",
                                    ["targetElements"] = new JArray("textToToggle", "imageToToggle", "imageToToggle2")
                                },
                                new JObject
                                {
                                    ["type"] = "Action.ToggleVisibility",
                                    ["title"] = "Show!",
                                    ["targetElements"] = new JArray(
                                        new JObject
                                        {
                                            ["elementId"] = "textToToggle",
                                            ["isVisible"] = true
                                        },
                                        new JObject
                                        {
                                            ["elementId"] = "imageToToggle",
                                            ["isVisible"] = true
                                        },
                                        new JObject
                                        {
                                            ["elementId"] = "imageToToggle2",
                                            ["isVisible"] = true
                                        }
                                    )
                                },
                                new JObject
                                {
                                    ["type"] = "Action.ToggleVisibility",
                                    ["title"] = "Hide!",
                                    ["targetElements"] = new JArray(
                                        new JObject
                                        {
                                            ["elementId"] = "textToToggle",
                                            ["isVisible"] = false
                                        },
                                        new JObject
                                        {
                                            ["elementId"] = "imageToToggle",
                                            ["isVisible"] = false
                                        },
                                        new JObject
                                        {
                                            ["elementId"] = "imageToToggle2",
                                            ["isVisible"] = false
                                        }
                                    )
                                }
                            }
                        }
                    }
                }
            };
            return card.ToString();
        }
    }
}