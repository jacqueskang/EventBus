using JKang.Events;
using JKang.Events.RabbitMq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RabbitMqServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMqEvents(this IServiceCollection services)
        {
            return services
                .AddSingleton<IEventPublisher, RabbitMqEventBus>()
                ;
        }
    }
}
