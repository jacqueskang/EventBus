using JKang.EventBus.AmazonSns;

namespace Samples.EventBus.Events
{
    [AmazonSnsTopic("my-message")]
    public class MessageSent
    {
        public MessageSent(string message) => Message = message;

        public string Message { get; }
    }
}
