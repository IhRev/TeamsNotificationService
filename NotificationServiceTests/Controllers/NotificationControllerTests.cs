using AutoMapper;
using TeamsNotificationService.Controllers;
using TeamsNotificationService.Framework;
using NSubstitute;
using TeamsNotificationService.DTOs;
using TeamsNotificationService.Core;
using Microsoft.AspNetCore.Mvc;

namespace NotificationServiceTests.Controllers
{
    [TestClass()]
    public class NotificationControllerTests
    {
        private INotificator notificator;
        private IMapper mapper;
        private NotificationController sut;
        private NotificationDTO notificationDTO;

        [TestInitialize()]
        public void Setup()
        {
            notificator = Substitute.For<INotificator>();
            mapper = Substitute.For<IMapper>();
            sut = new(notificator, mapper);
            notificationDTO = new()
            {
                JsonContent = "content",
                Recipient = "recipient",
                SourceSystem = "sourceSystem"
            };
        }

        [TestMethod()]
        public async Task SendNotificaction_ReturnsOkResult_IfNoExceptions()
        {
            //Arrange
            Notification notification = new()
            {
                SourceSystem = notificationDTO.SourceSystem,
                Recipient = notificationDTO.Recipient,
                JsonContent = notificationDTO.JsonContent
            };

            mapper.Map<Notification>(notificationDTO).Returns(notification);

            //Act
            var actual = await sut.SendNotificaction(notificationDTO);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(OkResult));
        }

        [TestMethod()]
        public async Task SendNotificaction_ReturnsBadRequest_IfMappingFailed()
        {
            //Arrange
            Notification notification = new()
            {
                SourceSystem = notificationDTO.SourceSystem,
                Recipient = notificationDTO.Recipient,
                JsonContent = notificationDTO.JsonContent
            };

            mapper.When(_ => _.Map<Notification>(notificationDTO)).Throw<AutoMapperMappingException>();

            //Act
            var actual = await sut.SendNotificaction(notificationDTO);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod()]
        public async Task SendNotificaction_ReturnsBadRequest_IfSendingFailed()
        {
            //Arrange
            Notification notification = new()
            {
                SourceSystem = notificationDTO.SourceSystem,
                Recipient = notificationDTO.Recipient,
                JsonContent = notificationDTO.JsonContent
            };

            mapper.Map<Notification>(notificationDTO).Returns(notification);
            notificator.When(_ => _.SendNotification(notification)).Throw<Exception>();

            //Act
            var actual = await sut.SendNotificaction(notificationDTO);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }
    }
}