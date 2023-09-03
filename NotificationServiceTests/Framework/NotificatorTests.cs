using TeamsNotificationService.Framework;
using TeamsNotificationService.Framework.Implementations;
using TeamsNotificationService.Services;
using NSubstitute;
using TeamsNotificationService.Core;
using TeamsNotificationService.Exceptions;

namespace NotificationServiceTests.Framework
{
    [TestClass()]
    public class NotificatorTests
    {
        private INotificator sut;
        private IEnumerable<INotificationSender> senders;
        private INotificationSender firstSender;
        private INotificationSender secondSender;

        [TestInitialize()]
        public void Setup()
        {
            firstSender = Substitute.For<INotificationSender>();
            firstSender.SourceSystem.Returns("first");
            secondSender = Substitute.For<INotificationSender>();
            secondSender.SourceSystem.Returns("second");
            senders = new[]
            {
                firstSender, secondSender
            };
            sut = new Notificator(senders);
        }

        [TestMethod()]
        public async Task SendNotification_GetSender_IfSenderFound()
        {
            //Arrange
            Notification notification = new()
            {
                SourceSystem = "second"
            };

            //Act
            await sut.SendNotification(notification);

            //Assert
            firstSender.Received(0);
            secondSender.Received(1);
        }

        [TestMethod()]
        public async Task SendNotification_ThrowException_IfSenderNotFound()
        {
            //Arrange
            Notification notification = new()
            {
                SourceSystem = "third"
            };

            //Act

            //Assert
            await Assert.ThrowsExceptionAsync<SourceSystemNotFoundException>(async ()
                => await sut.SendNotification(notification));
        }
    }
}