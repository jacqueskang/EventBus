using JKang.EventBus.MultiChannel;
using Microsoft.Extensions.DependencyInjection;

namespace JKang.EventBus.DependencyInjection
{
    public class EventBusBuilder : IEventBusBuilder
    {
        private readonly EventPublisherRegister _register = new EventPublisherRegister();

        public EventBusBuilder(IServiceCollection services)
        {
            Services = services
                .AddSingleton(_register)
                .AddSingleton<IEventPublisherProvider, EventPublisherProvider>()
                .AddSingleton<IEventPublisher, MasterEventBus>();
        }

        public IServiceCollection Services { get; }

        public IEventBusBuilder UseSerializer<TEventSerializer>()
            where TEventSerializer : class, IEventSerializer
        {
            Services.AddSingleton<IEventSerializer, TEventSerializer>();
            return this;
        }

        public IEventBusBuilder AddEventPublisher<TEventPublisher>()
            where TEventPublisher : class, IEventPublisher
        {
            _register.Add<TEventPublisher>();
            Services.AddSingleton<TEventPublisher>();
            return this;
        }
    }
}
