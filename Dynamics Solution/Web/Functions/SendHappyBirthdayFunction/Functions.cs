using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SB.WebShared.DynamicsAuthentication;

namespace SendHappyBirthdayFunction
{
    public class Functions
    {
        private const string
            TestsQueue = "%TestsQueue%",
            Cron= "%CRON_EXPRESSION%";


        private readonly IAuthenticationService _authenticationService;

        public Functions(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(IAuthenticationService));
        }

        [FunctionName("SendCongratulation")]
        public async Task SendCongratulationAsync([ServiceBusTrigger(TestsQueue)] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            var token = await _authenticationService.GetToken();
            var serviceUrl = _authenticationService.GetServiceUri();

            var json = JsonCreatorService.FormStringContent(myQueueItem, "SendBirthdayEmail");

            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync(serviceUrl, json);

            if (response.IsSuccessStatusCode)
            {
                log.LogInformation($"SendBirthdayEmail action success finished ");
            }
            else
            {
                log.LogInformation($"SendBirthdayEmail action finished with {response.StatusCode}");
            }
        }

        [FunctionName("SendMessagesToQueue")]
        public async Task Run([TimerTrigger(Cron)] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var token = await _authenticationService.GetToken();
            var serviceUrl = _authenticationService.GetServiceUri();

            var json = JsonCreatorService.FormStringContent("", "RetrieveBirthdayContacts");

            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync(serviceUrl, json);

            if (response.IsSuccessStatusCode)
            {
                log.LogInformation($"SendBirthdayEmail action success finished ");
            }
            else
            {
                log.LogInformation($"SendBirthdayEmail action finished with {response.StatusCode}");
            }
        }
    }
}
