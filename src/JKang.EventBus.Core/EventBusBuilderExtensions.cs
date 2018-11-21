﻿using JKang.EventBus;
using JKang.EventBus.InMemory;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EventBusBuilderExtensions
    {
        public static IEventBusBuilder AddInMemoryEventBus(this IEventBusBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services
                .AddSingleton<IEventPublisher, InMemoryEventBus>()
                ;

            return builder;
        }
    }
}