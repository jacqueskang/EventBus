using JKang.Events;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IEventBusBuilder
    {
        IServiceCollection Services { get; }

        IEventBusBuilder AddEventHandler<TEvent, TEventHandler>()
            where TEventHandler : class, IEventHandler<TEvent>;
    }
}
