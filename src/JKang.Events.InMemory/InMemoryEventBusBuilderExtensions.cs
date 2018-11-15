using JKang.Events;
using JKang.Events.InMemory;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InMemoryEventBusBuilderExtensions
    {
        public static IEventBusBuilder UseInMemory(this IEventBusBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services
                .AddSingleton<IEventPublisher, InMemoryEventBus>()
                ;

            return builder;
        }
    }
}
