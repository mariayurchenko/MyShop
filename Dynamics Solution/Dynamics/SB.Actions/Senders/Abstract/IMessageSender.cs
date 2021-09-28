using SB.Shared.Models.Actions;

namespace SB.Actions.Senders.Abstract
{
    public interface IMessageSender
    {
        ActionResponse SendMessage(string message, string entityPass);
        ActionResponse SendMessage(string message, string entityPass, int maxRetryCount, int retryCount = 0);
    }
}