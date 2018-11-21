using JKang.EventBus;
using JKang.EventBus.AmazonSns;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AmazonSnsEventBusBuilderExtensions
    {
        public static IEventBusBuilder AddAmazonSnsEventPublisher(this IEventBusBuilder builder,
            Action<AmazonSnsEventBusOptions> setupActions)
        {
            builder.Services
                .Configure(setupActions)
                ;

            return builder
                .AddEventPublisher<AmazonSnsEventBus>();
        }
    }
}
