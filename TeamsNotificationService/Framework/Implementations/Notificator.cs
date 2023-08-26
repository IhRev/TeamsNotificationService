using TeamsNotificationService.Core;
using TeamsNotificationService.Exceptions;
using TeamsNotificationService.Services;

namespace TeamsNotificationService.Framework.Implementations
{
    public class Notificator : INotificator
    {
        private readonly Dictionary<string, INotificationSender> notificationSenders;

        public Notificator(IEnumerable<INotificationSender> senders)
        {
            notificationSenders = senders.ToDictionary(sender => sender.SourceSystem);
        }

        public async Task SendNotification(Notification notificationData)
        {
            if (notificationSenders.TryGetValue(notificationData.SourceSystem, out var sender))
            {
                await sender.SendNotification(notificationData);
            }
            else
            {
                throw new SourceSystemNotFoundException("Notification source system is not supported");
            }
        }
    }
}