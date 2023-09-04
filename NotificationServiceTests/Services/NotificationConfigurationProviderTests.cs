using TeamsNotificationService.Services;
using TeamsNotificationService.Services.Implementations;
using TeamsNotificationService.System;
using NSubstitute;
using TeamsNotificationService.Exceptions;

namespace NotificationServiceTests.Services
{
    [TestClass()]
    public class NotificationConfigurationProviderTests
    {
        private string notificationServiceName = "name";
        private string appPath = "appPath/";
        private string configurationPath = "appPath/NotificationServiceConfigurations/name.json";
        private IConfigurationService sut = null!;
        private IJsonWrapper jsonWrapper = null!;
        private IIOWrapper iOWrapper = null!;

        [TestInitialize()]
        public void Setup()
        {
            jsonWrapper = Substitute.For<IJsonWrapper>();
            iOWrapper = Substitute.For<IIOWrapper>();
            iOWrapper.AppPath.Returns(appPath);
            sut = new NotificationConfigurationProvider(jsonWrapper, iOWrapper);
        }

        [TestMethod()]
        public async Task GetConfigurationAsync_ReturnsConfiguration_IfExists()
        {
            //Arrange
            TestConfiguration expected = new TestConfiguration() { TestProperty = "testProp" };

            string configAsString = "configAsString";
            iOWrapper.ReadAllTextAsync(configurationPath).Returns(configAsString);

            jsonWrapper.Deserialize<TestConfiguration>(configAsString).Returns(expected);

            //Act
            TestConfiguration actual = await sut
                .GetConfigurationAsync<TestConfiguration>(notificationServiceName);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task GetConfigurationAsync_ThrowsException_IfMotExists()
        {
            //Arrange
            iOWrapper.ReadAllTextAsync(configurationPath).Returns("");

            //Act

            //Assert
            await Assert.ThrowsExceptionAsync<ConfigurationNotFoundException>(async () => await sut
                .GetConfigurationAsync<TestConfiguration>(notificationServiceName));
        }

        private class TestConfiguration
        {
            public string TestProperty { get; set; } = null!;
        }
    }
}