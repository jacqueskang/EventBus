using System;
using System.Collections.Generic;

namespace JKang.EventBus
{
    public interface IEventPublisherProvider
    {
        IEnumerable<IEventPublisher> GetEventPublishers();
    }
}
