# JKang.Events

A .NET Core ultra lightweight in-memory event bus implementation.

## NuGet packages

 - [JKang.Events.Abstractions](https://www.nuget.org/packages/JKang.Events.Abstractions/)
 - [JKang.Events.InMemory](https://www.nuget.org/packages/JKang.Events.InMemory/)

## Sample:

1. Create an event implementing IEvent interface

```csharp
    public class MessageSent : IEvent
    {
        public MessageSent(Guid eventId, string message)
        {
            EventId = eventId;
            Message = message;
        }

        public Guid EventId { get; }

        public string Message { get; }
```

2. Implement one or multiple event handlers

```csharp
    public class MessageSentEventHandler : IEventHandler<MessageSent>
    {
        public Task HandleEventAsync(MessageSent @event)
        {
            // consume the event here
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
            .AddInMemoryEvents()
            .AddScoped<IEventHandler<MessageSent>, MessageSentEventHandler>()
            ;
    }
```

4. Publish the event

```csharp
    var @event = MessageSent.Create("Hello world!");
    await _eventPublisher.PublishEventAsync(@event);
```

Any contributions or comments are welcome!

