using JKang.EventBus;
using JKang.EventBus.AmazonSns;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AmazonSnsEventBusBuilderExtensions
    {
        public static IEventBusBuilder UseAmazonSns(this IEventBusBuilder builder, 
            Action<AmazonSnsEventBusOptions> setupActions)
        {
            builder.Services
                .AddSingleton<IEventPublisher, AmazonSnsEventBus>()
                .Configure(setupActions)
                ;
            return builder;
        }
    }
}
