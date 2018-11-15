namespace JKang.EventBus.Samples.InMemory.AspNetCore.Events
{
    public class MessageSent
    {
        public MessageSent(string message) => Message = message;

        public string Message { get; }
    }
}
