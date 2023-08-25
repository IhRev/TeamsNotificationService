using TeamsNotificationService.Core;

namespace TeamsNotificationService.Framework
{
    public interface INotificator
    {
        public Task SendNotification(Notification notificationData);
    }
}