# JKang.Events

A .NET Core ultra lightweight in-memory event bus implementation.

## NuGet packages

 - [JKang.Events](https://www.nuget.org/packages/JKang.Events/)

## Sample:

1. Create an event class

```csharp
    public class MessageSent
    {
        public MessageSent(string message) => Message = message;

        public string Message { get; }
    }
```

2. Implement as many event handlers as you like

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
            .AddEventBus()
            .UseInMemory()
            .AddEventHandler<MessageSent, MessageSentEventHandler>()
        ;
    }
```

4. Publish the event

```csharp
    await _eventPublisher.PublishEventAsync(new MessageSent("Something happened!"));
```

Any contributions or comments are welcome!

