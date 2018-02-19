using System.Threading.Tasks;

namespace JKang.Events
{
    public interface IEventPublisher
    {
        Task PublishEventAsync(IEvent @event);
    }
}
