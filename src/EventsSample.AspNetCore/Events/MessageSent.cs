using System;

namespace JKang.Events.Samples.InMemory.AspNetCore.Events
{
    public class MessageSent
    {
        public static MessageSent Create(string message)
        {
            return new MessageSent(Guid.NewGuid(), message);
        }

        private MessageSent(Guid eventId, string message)
        {
            EventId = eventId;
            Message = message;
        }

        public Guid EventId { get; }
        public string Message { get; }
    }
}
