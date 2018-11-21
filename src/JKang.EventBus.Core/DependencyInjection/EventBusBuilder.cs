using JKang.EventBus.Serialization;
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

        public IEventBusBuilder UseSerializer<TEventSerializer>()
            where TEventSerializer : class, IEventSerializer
        {
            Services
                .AddSingleton<IEventSerializer, TEventSerializer>();
            return this;
        }

        IEventBusBuilder IEventBusBuilder.AddEventHandler<TEvent, TEventHandler>()
        {
            Services
                .AddScoped<IEventHandler<TEvent>, TEventHandler>()
                ;
            return this;
        }
    }
}
