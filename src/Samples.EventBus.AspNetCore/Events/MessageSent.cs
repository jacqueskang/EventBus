using JKang.EventBus.AmazonSns;

namespace JKang.EventBus.Samples.InMemory.AspNetCore.Events
{
    [AmazonSnsTopic("my-message")]
    public class MessageSent
    {
        public MessageSent(string message) => Message = message;

        public string Message { get; }
    }
}
