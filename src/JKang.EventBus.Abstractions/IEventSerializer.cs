namespace JKang.EventBus
{
    public interface IEventSerializer
    {
        string Serialize<TEvent>(TEvent @event);
    }
}
