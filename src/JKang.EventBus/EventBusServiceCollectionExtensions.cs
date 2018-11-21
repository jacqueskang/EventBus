using JKang.EventBus.DependencyInjection;
using JKang.EventBus.Serialization;
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
                .UseSerializer<JsonEventSerializer>()
                .UseInMemory()
                ;

            setupAction?.Invoke(builder);

            return services;
        }

        [Obsolete]
        public static IEventBusBuilder AddEventBus(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return new EventBusBuilder(services)
                .UseSerializer<JsonEventSerializer>()
                ;
        }
    }
}
