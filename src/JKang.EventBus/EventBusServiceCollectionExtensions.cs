using JKang.EventBus.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EventBusServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services,
            Action<IEventBusBuilder> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            IEventBusBuilder builder = new EventBusBuilder(services)
                .UseJsonSerializer()
                ;

            setupAction?.Invoke(builder);

            return services;
        }
    }
}
