using Microsoft.Xrm.Sdk;
using SB.Shared.Models.Actions;
using SB.Actions.Senders.Abstract;
using System.Net;
using System.Text;

namespace SB.Actions.Senders.QueueMessageSender
{
    public class QueueMessageSender : IMessageSender
    {
        private readonly string connectionString;
        private readonly ITracingService _tracer;

        public QueueMessageSender(string connectionString, ITracingService tracer)
        {
            this.connectionString = connectionString;
            _tracer = tracer;
        }

        public ActionResponse SendMessage(string message, string entityPass)
        {
            using (var sender = new HttpQueueSender($"{connectionString};EntityPath={entityPass}"))
            {
                var httpResponse = sender.SendAsync(Encoding.UTF8.GetBytes(message)).GetAwaiter().GetResult();

                if (httpResponse.IsSuccessStatusCode)
                {
                    return new ActionResponse();
                }

                return new ActionResponse
                {
                    Status = Status.Error,
                    Value = "HttpQueueSender Error"
                };
            }
        }

        public ActionResponse SendMessage(string message, string entityPass, int maxRetryCount, int retryCount = 0)
        {
            try
            {
                return SendMessage(message, entityPass);
            }
            catch (WebException wex)
            {
                _tracer?.Trace(wex.Message);

                if (wex.Message.Equals("Unable to connect to the remote server") && retryCount < maxRetryCount)
                {
                    return SendMessage(message, entityPass, maxRetryCount, ++retryCount);
                }

                throw;
            }
        }
    }
}