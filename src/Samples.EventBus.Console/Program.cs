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
                    .AddInMemoryEventBus(subscriber =>
                    {
                        subscriber.Subscribe<MyEvent, MyEventHandler>();
                        //subscriber.SubscribeAllHandledEvents<MyEventHandler>(); // other way
                    })
                    .PublishToAmazonSns(x => x.Region = "eu-west-3")
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
