# JKang.Events

A .NET Core ultra lightweight in-memory event bus implementation.

## Sample:

1. Create an event implementing IEvent interface

```csharp
    public class MessageSent : IEvent
    {
        public static MessageSent Create(string message)
        {
            return new MessageSent(Guid.NewGuid(), message);
        }

        private MessageSent(Guid eventId, string message)
        {
            EventId = eventId;
            Message = message;
        }

        public Guid EventId { get; }

        public string Message { get; }
```

2. Implement an event handler and register it IoC

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

3. Publish the event

```csharp
    public class IndexModel : PageModel
    {
        private readonly IEventPublisher _eventPublisher;

        public IndexModel(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        [BindProperty]
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var @event = MessageSent.Create(Message);
            await _eventPublisher.PublishEventAsync(@event);

            return RedirectToPage();
        }
    }
```

Any contributions or comments are welcome!

