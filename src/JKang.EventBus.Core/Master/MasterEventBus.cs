using System;
using System.Threading.Tasks;

namespace JKang.EventBus.MultiChannel
{
    internal class MasterEventBus : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventPublisherProvider _eventPublishers;

        public MasterEventBus(
            IEventPublisherProvider eventPublishers)
        {
            _eventPublishers = eventPublishers;
        }

        public async Task PublishEventAsync<TEvent>(TEvent @event)
        {
            foreach (IEventPublisher publisher in _eventPublishers.GetEventPublishers())
            {
                await publisher.PublishEventAsync(@event);
            }
        }
    }
}
