using JKang.EventBus;
using System;
using System.Threading.Tasks;

namespace Samples.EventBus.ConsoleApp
{
    class MyEventHandler : IEventHandler<MyEvent>
    {
        public Task HandleEventAsync(MyEvent @event)
        {
            Console.WriteLine($"Received message '{@event.Message}'");
            return Task.CompletedTask;
        }
    }
}
