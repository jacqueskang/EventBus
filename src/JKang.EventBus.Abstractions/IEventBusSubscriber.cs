namespace JKang.EventBus
{
    public interface IEventBusSubscriber
    {
        IEventBusSubscriber Subscribe<TEvent, TEventHandler>()
            where TEvent: class
            where TEventHandler: class, IEventHandler<TEvent>;

        IEventBusSubscriber SubscribeAllHandledEvents<TEventHandler>()
            where TEventHandler : class;
    }
}
