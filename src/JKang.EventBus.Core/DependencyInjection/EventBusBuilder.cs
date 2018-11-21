using JKang.EventBus.MultiChannel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEventBusBuilder AddEventHandler<TEvent, TEventHandler>()
            where TEventHandler : class, IEventHandler<TEvent>
        {
            Services.AddScoped<IEventHandler<TEvent>, TEventHandler>();
            return this;
        }

        public IEventBusBuilder AddEventHandler<TEventHandler>()
            where TEventHandler : class
        {
            Type implementationType = typeof(TEventHandler);
            IEnumerable<Type> serviceTypes = implementationType
                .GetInterfaces()
                .Where(i => i.IsGenericType)
                .Where(i => i.GetGenericTypeDefinition() == typeof(IEventHandler<>));

            foreach (Type serviceType in serviceTypes)
            {
                Services.AddScoped(serviceType, implementationType);
            }

            return this;
        }
    }
}
