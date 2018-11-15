using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace JKang.EventBus.InMemory
{
    public class InMemoryEventBus : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishEventAsync<TEvent>(TEvent @event)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                Type eventType = @event.GetType();
                Type openHandlerType = typeof(IEventHandler<>);
                Type handlerType = openHandlerType.MakeGenericType(eventType);
                IEnumerable<object> handlers = scope.ServiceProvider.GetServices(handlerType);
                foreach (object handler in handlers)
                {
                    object result = handlerType
                        .GetTypeInfo()
                        .GetDeclaredMethod(nameof(IEventHandler<TEvent>.HandleEventAsync))
                        .Invoke(handler, new object[] { @event });
                    await (Task)result;
                }
            }
        }
    }
}
