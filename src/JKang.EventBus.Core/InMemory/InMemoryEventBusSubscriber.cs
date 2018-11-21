using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JKang.EventBus.Subscription
{
    class InMemoryEventBusSubscriber : IEventBusSubscriber
    {
        private readonly IServiceCollection _services;

        public InMemoryEventBusSubscriber(IServiceCollection services)
        {
            _services = services;
        }

        public IEventBusSubscriber Subscribe<TEvent, TEventHandler>()
            where TEvent : class
            where TEventHandler : class, IEventHandler<TEvent>
        {
            _services.AddScoped<IEventHandler<TEvent>, TEventHandler>();
            return this;
        }

        public IEventBusSubscriber SubscribeAllHandledEvents<TEventHandler>()
            where TEventHandler : class
        {
            Type implementationType = typeof(TEventHandler);
            IEnumerable<Type> serviceTypes = implementationType
                .GetInterfaces()
                .Where(i => i.IsGenericType)
                .Where(i => i.GetGenericTypeDefinition() == typeof(IEventHandler<>));

            foreach (Type serviceType in serviceTypes)
            {
                _services.AddScoped(serviceType, implementationType);
            }

            return this;
        }
    }
}
