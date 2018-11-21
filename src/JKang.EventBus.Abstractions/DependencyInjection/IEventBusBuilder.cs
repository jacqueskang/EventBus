using JKang.EventBus;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IEventBusBuilder
    {
        IServiceCollection Services { get; }

        IEventBusBuilder UseSerializer<TEventSerializer>()
            where TEventSerializer : class, IEventSerializer;

        IEventBusBuilder AddEventHandler<TEvent, TEventHandler>()
            where TEventHandler : class, IEventHandler<TEvent>;

        IEventBusBuilder AddEventHandler<TEventHandler>()
            where TEventHandler : class;

        IEventBusBuilder AddEventPublisher<TEventPublisher>()
            where TEventPublisher : class, IEventPublisher;
    }
}
