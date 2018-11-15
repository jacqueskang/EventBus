using JKang.EventBus.DependencyInjection;
using JKang.Events;
using JKang.Events.RabbitMq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RabbitMqEventBusBuilderExtensions
    {
        public static IEventBusBuilder UseRabbitMq(this IEventBusBuilder builder)
        {
            builder.Services
                .AddSingleton<IEventPublisher, RabbitMqEventBus>()
                ;
            return builder;
        }
    }
}
