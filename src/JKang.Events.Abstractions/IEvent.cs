using System;

namespace JKang.Events
{
    public interface IEvent
    {
        Guid EventId { get; }
    }
}
