using JKang.EventBus;
using JKang.EventBus.AmazonSns;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AmazonSnsEventBusBuilderExtensions
    {
        public static IEventBusBuilder PublishToAmazonSns(this IEventBusBuilder builder,
            Action<AmazonSnsEventPublisherOptions> setupActions)
        {
            builder.Services
                .Configure(setupActions)
                ;

            return builder
                .AddEventPublisher<AmazonSnsEventPublisher>();
        }
    }
}
