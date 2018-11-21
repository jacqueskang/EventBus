using JKang.EventBus;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IEventBusBuilder
    {
        IServiceCollection Services { get; }

        IEventBusBuilder UseSerializer<TEventSerializer>()
            where TEventSerializer : class, IEventSerializer;

        IEventBusBuilder AddEventPublisher<TEventPublisher>()
            where TEventPublisher : class, IEventPublisher;
    }
}
