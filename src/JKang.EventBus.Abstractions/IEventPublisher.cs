using System.Threading.Tasks;

namespace JKang.EventBus
{
    public interface IEventPublisher
    {
        Task PublishEventAsync<TEvent>(TEvent @event);
    }
}
