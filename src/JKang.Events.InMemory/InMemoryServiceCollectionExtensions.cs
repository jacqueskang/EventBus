using JKang.Events;
using JKang.Events.InMemory;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InMemoryServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryEvents(this IServiceCollection services)
        {
            return services
                .AddSingleton<IEventPublisher, InMemoryEventBus>()
                ;
        }
    }
}
