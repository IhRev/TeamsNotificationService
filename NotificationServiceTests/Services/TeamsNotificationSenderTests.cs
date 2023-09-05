using TeamsNotificationService.Services;
using TeamsNotificationService.System;
using NSubstitute;
using TeamsNotificationService.Services.Implementations;
using TeamsNotificationService.Core;
using AdaptiveCards;
using TeamsNotificationService.Models;
using TeamsNotificationService.Exceptions;

namespace NotificationServiceTests.Services
{
    [TestClass()]
    public class TeamsNotificationSenderTests
    {
        private string sourceSystem = "source";
        private string recipient = "recipient";
        private INotificationSender sut = null!;
        private IConfigurationService configurationService = null!;
        private IHttpWrapper httpWrapper = null!;
        private Notification notification = null!;

        [TestInitialize()]
        public void Setup()
        {
            configurationService = Substitute.For<IConfigurationService>();
            httpWrapper = Substitute.For<IHttpWrapper>();
            sut = new TeamsNotificationSender(configurationService, httpWrapper);
            notification = new()
            {
                SourceSystem = sourceSystem,
                Recipient = recipient
            };
        }

        [TestMethod()]
        public async Task SendNotification_Success_IfRecepientFoundAndNotificationSent()
        {
            //Arrange
            HttpResponseMessage httpResponse = new()
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };

            NotificationConfiguration config = new NotificationConfiguration()
            {
                RecipientName = recipient,
                WebhookUrl = "url"
            };

            configurationService
                .GetConfigurationAsync<IEnumerable<NotificationConfiguration>>(sourceSystem)
                .Returns(new List<NotificationConfiguration>() { config });

            httpWrapper.PostAsync(config.WebhookUrl, Arg.Any<StringContent>()).Returns(httpResponse);

            //Act
            await sut.SendNotification(notification);

            //Assert

        }

        [TestMethod()]
        public async Task SendNotification_ThroException_IfRicipientIsNotFound()
        {
            //Arrange
            NotificationConfiguration config = new NotificationConfiguration()
            {
                RecipientName = "",
                WebhookUrl = "url"
            };

            configurationService
                .GetConfigurationAsync<IEnumerable<NotificationConfiguration>>(sourceSystem)
                .Returns(new List<NotificationConfiguration>() { config });

            //Act

            //Assert
            await Assert
                .ThrowsExceptionAsync<SourceNotFoundException>(async () =>
                await sut.SendNotification(notification));
        }
        
        [TestMethod()]
        public async Task SendNotification_ThroException_IfHttpResponseMessageIsNotSuccess()
        {
            //Arrange
            HttpResponseMessage httpResponse = new()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            NotificationConfiguration config = new NotificationConfiguration()
            {
                RecipientName = recipient,
                WebhookUrl = "url"
            };

            configurationService
                .GetConfigurationAsync<IEnumerable<NotificationConfiguration>>(sourceSystem)
                .Returns(new List<NotificationConfiguration>() { config });

            httpWrapper.PostAsync(config.WebhookUrl, Arg.Any<StringContent>()).Returns(httpResponse);

            //Act

            //Assert
            await Assert
                .ThrowsExceptionAsync<HttpRequestException>(async () =>
                await sut.SendNotification(notification));
        }
    }
}