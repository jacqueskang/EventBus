using JKang.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Samples.EventBus.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddEventBus(builder =>
            {
                builder
                    .AddInMemoryEventBus()
                    .AddEventHandler<MyEventHandler>()
                    .AddAmazonSnsEventPublisher(x => x.Region = "eu-west-3")
                    ;
            });

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IEventPublisher eventPublisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();
                eventPublisher.PublishEventAsync(new MyEvent { Message = "Hello, event bus!" }).Wait();
            }
        }
    }
}
