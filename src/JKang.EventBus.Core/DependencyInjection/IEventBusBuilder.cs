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
    }
}
