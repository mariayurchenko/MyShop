using System.Threading.Tasks;
using Data.DTO;
using FakeItEasy;
using Microsoft.Azure.WebJobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SB.WebShared.DynamicsAuthentication;

namespace SendHappyBirthdayFunction.Tests
{
    [TestClass]
    public class FunctionsTests
    {
        private Moq.Mock<Microsoft.Extensions.Logging.ILogger<Functions>> _loggerMock;
        private readonly CrmClientOptions _crmClientOptions = TestFactory.CreateCrmClientOptions();

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<Functions>>();
        }

        [TestMethod]
        public async Task SuccessfulExecutionOfTheSendCongratulation()
        {
            var service = new AuthenticationService(_crmClientOptions);

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.GetServiceUri()).Returns(service.GetServiceUri());
            authenticationService.Setup(x => x.GetToken()).ReturnsAsync(await service.GetToken());

            var functions = new Functions(authenticationService.Object);
            await functions.SendCongratulationAsync("8b28032a-e4c1-eb11-bacc-000d3abf0037", _loggerMock.Object);
        }

        [TestMethod]
        public async Task SuccessfulExecutionOfTheSendMessagesToQueue()
        {
            var service = new AuthenticationService(_crmClientOptions);

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.GetServiceUri()).Returns(service.GetServiceUri());
            authenticationService.Setup(x => x.GetToken()).ReturnsAsync(await service.GetToken());

            var fakeTimerInfo = A.Fake<TimerInfo>();

            var functions = new Functions(authenticationService.Object);
            await functions.Run(fakeTimerInfo, _loggerMock.Object);
        }
    }
}
