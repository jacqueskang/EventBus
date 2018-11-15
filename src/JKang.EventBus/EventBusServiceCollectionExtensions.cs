using JKang.EventBus.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EventBusServiceCollectionExtensions
    {
        public static IEventBusBuilder AddEventBus(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return new EventBusBuilder(services);
        }
    }
}
