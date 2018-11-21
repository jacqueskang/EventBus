# JKang.EventBus

.NET Core event bus implementation supporting combination of the following channels:
 * In-memory event dispatching
 * publishing event to Amazon SNS

## NuGet packages

 - [JKang.EventBus](https://www.nuget.org/packages/JKang.EventBus/)

## Sample:

1. Create an event class

```csharp
    public class MessageSent
    {
        public MessageSent(string message) => Message = message;

        public string Message { get; }
    }
```

2. Implement event handlers (at the time being only receives events from in-memory event bus)

```csharp
    public class MessageSentEventHandler : IEventHandler<MessageSent>
    {
        public Task HandleEventAsync(MessageSent @event)
        {
			Console.WriteLine(@event.Message)
            return Task.CompletedTask;
        }
    }
```

3. register event handlers in IServiceCollection

```csharp
    // Startup.cs
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddEventBus(builder =>
            {
                builder
                    .AddInMemoryEventBus()
                    .AddAmazonSnsEventPublisher(x => x.Region = "eu-west-3")
                    .AddEventHandler<MessageSent, MessageSentEventHandler>()
                    ;
            });
    }
```

4. Publish the event

```csharp
    await _eventPublisher.PublishEventAsync(new MessageSent("Something happened!"));
```

Any contributions or comments are welcome!

