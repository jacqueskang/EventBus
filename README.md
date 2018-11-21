# JKang.EventBus

.NET Core event bus implementation supporting combination of the following channels:
 * In-memory event dispatching
 * publishing event to Amazon SNS

## NuGet packages

 - [JKang.EventBus](https://www.nuget.org/packages/JKang.EventBus/)
 - [JKang.EventBus.AmazonSns](https://www.nuget.org/packages/JKang.EventBus.AmazonSns/)

## Quick start:

1. Create a console application with the following NuGet packages installed:
```shell
> Install-Package Microsoft.Extensions.DependencyInjection
> Install-Package JKang.EventBus
```

 2. Create an event class

```csharp
    public class MyEvent
    {
        public string Message { get; set; }
    }
```

3. Optionally implement an handler to subscribe to the event published from memory bus

```csharp
    class MyEventHandler : IEventHandler<MyEvent>
    {
        public Task HandleEventAsync(MyEvent @event)
        {
            Console.WriteLine($"Received message '{@event.Message}'");
            return Task.CompletedTask;
        }
    }
```

4. Configure and register event bus using Dependency Injection

```csharp
    static void Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();

        services.AddEventBus(builder =>
        {
            builder
                .AddInMemoryEventBus()
                .AddEventHandler<MyEventHandler>()
                ;
        });
    }
```

5. Publish an event

```csharp
    IServiceProvider serviceProvider = services.BuildServiceProvider();
    using (IServiceScope scope = serviceProvider.CreateScope())
    {
        IEventPublisher eventPublisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();
        eventPublisher.PublishEventAsync(new MyEvent { Message = "Hello, event bus!" }).Wait();
    }
```


## Publish event to Amazon Simple Notification Service (SNS)

1. Install NuGet package **JKang.EventBus.AmazonSns**

2. Configure the event bus as following:
```csharp
        services.AddEventBus(builder =>
        {
            builder
                .AddAmazonSnsEventPublisher(x => x.Region = "eu-west-3")
                ;
        });
```

It's possible to publish events to multiple event buses
```csharp
        services.AddEventBus(builder =>
        {
            builder
                .AddInMemoryEventBus()
                .AddAmazonSnsEventPublisher(x => x.Region = "eu-west-3")
                ;
        });
```

Any contributions or comments are welcome!

