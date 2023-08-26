using TeamsNotificationService.Core;
using TeamsNotificationService.Models;

namespace TeamsNotificationService.Services.Implementations
{
	public class TeamsNotificationSender : INotificationSender
	{
        private readonly IConfigurationService configurationService;

        public TeamsNotificationSender(IConfigurationService configurationService)
		{
            this.configurationService = configurationService;
        }

        public string SourceSystem => "Teams";

        public async Task SendNotification(Notification notificationData)
        {
            NotificationConfiguration configuration = await configurationService
               .GetConfigurationAsync<NotificationConfiguration>(notificationData.SourceSystem);
        }
    }
}