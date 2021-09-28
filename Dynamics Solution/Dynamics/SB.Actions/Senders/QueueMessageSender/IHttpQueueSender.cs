using System.Net.Http;
using System.Threading.Tasks;

namespace SB.Actions.Senders.QueueMessageSender
{
    public interface IHttpQueueSender
    {
        Task<HttpResponseMessage> SendAsync(byte[] body);
    }
}