using JKang.EventBus.AmazonSqs;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AmazonSqsEventBusBuilderExtensions
    {
        public static IEventBusBuilder PublishToAmazonSqs(this IEventBusBuilder builder,
            Action<AmazonSqsEventPublisherOptions> setupActions)
        {
            builder.Services
                .Configure(setupActions)
                ;

            return builder
                .AddEventPublisher<AmazonSqsEventPublisher>();
        }
    }
}
