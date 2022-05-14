# JKang.EventBus

.NET Core event bus implementation supporting:
 * In-memory event dispatching (publishing and subscription)
 * publishing event to Amazon SNS
 * publishing event to Amazon SQS

## NuGet packages

 - [JKang.EventBus](https://www.nuget.org/packages/JKang.EventBus/)

## Quick start:

1. Create a console application with the following NuGet packages installed:
```console
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

3. Optionally implement one or multiple handlers subscribing to the event

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
            builder.AddInMemoryEventBus(subscriber =>
            {
                subscriber.Subscribe<MyEvent, MyEventHandler>();
                //subscriber.SubscribeAllHandledEvents<MyEventHandler>(); // other way
            });
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

1. Install NuGet package [JKang.EventBus.AmazonSns](https://www.nuget.org/packages/JKang.EventBus.AmazonSns/)

2. Configure publishing events to AWS SNS

```csharp
        services.AddEventBus(builder =>
        {
            builder
                //.AddInMemoryEventBus() // uncomment to keep using in-memory event bus in the same time
                .PublishToAmazonSns(x => x.Region = "eu-west-3");
        });
```

3. Optionally It's possible to customize AWS SNS topic name using annotation

```csharp
    [AmazonSnsTopic("my-event")]
    public class MyEvent { }
```



## Publish event to Amazon Simple Queue Service (SQS)

1. Install NuGet package [JKang.EventBus.AmazonSqs](https://www.nuget.org/packages/JKang.EventBus.AmazonSqs/)

2. Configure publishing events to AWS SQS

```csharp
        services.AddEventBus(builder =>
        {
            builder
                //.AddInMemoryEventBus() // uncomment to keep using in-memory event bus in the same time
                .PublishToAmazonSqs(options =>
                    {
                        options.AccessKeyId = "";
                        options.SecretAccessKey = "";
                        options.DefaultQueueUrl = "";
                        options.Region = "us-east-1";
                    });
        });
```

3. Optionally It's possible to customize AWS SQS queue url using annotation

```csharp
    [AmazonSqsQueue("https://sqs.ap-southeast-2.amazonaws.com/189107071895/youtube-demo")]
    public class MyEvent { }
```


Any contributions or comments are welcome!
