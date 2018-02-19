using System;
using System.Threading.Tasks;

namespace JKang.Events.InMemory
{
    public class InMemoryEventBus : IEventPublisher
    {
        public Task PublishEventAsync(IEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
