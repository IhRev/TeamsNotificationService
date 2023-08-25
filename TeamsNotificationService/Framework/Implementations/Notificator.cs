using TeamsNotificationService.Core;
using TeamsNotificationService.Models;
using TeamsNotificationService.Services;

namespace TeamsNotificationService.Framework.Implementations
{
    public class Notificator : INotificator
    {
        private readonly IConfigurationService configurationService;

        public Notificator(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public async Task SendNotification(Notification notificationData)
        {
            NotificationConfiguration configuration = await configurationService.GetConfigurationAsync(notificationData.SourceSystem);

        }
    }
}