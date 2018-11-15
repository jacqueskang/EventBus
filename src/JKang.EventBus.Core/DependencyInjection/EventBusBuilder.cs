using JKang.Events;
using Microsoft.Extensions.DependencyInjection;

namespace JKang.EventBus.DependencyInjection
{
    public class EventBusBuilder : IEventBusBuilder
    {
        public EventBusBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        IEventBusBuilder IEventBusBuilder.AddEventHandler<TEvent, TEventHandler>()
        {
            Services.AddScoped<IEventHandler<TEvent>, TEventHandler>();
            return this;
        }
    }
}
