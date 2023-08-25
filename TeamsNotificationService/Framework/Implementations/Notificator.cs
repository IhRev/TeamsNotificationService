using TeamsNotificationService.Core;

namespace TeamsNotificationService.Framework.Implementations
{
    public class Notificator : INotificator
    {
        public Notificator()
        {
        }

        public Task SendNotification(Notification notificationData)
        {
            return Task.FromResult(0);
        }
    }
}