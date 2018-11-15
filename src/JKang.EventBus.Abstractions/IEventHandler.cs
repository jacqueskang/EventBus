using System.Threading.Tasks;

namespace JKang.EventBus
{
    public interface IEventHandler<TEvent>
    {
        Task HandleEventAsync(TEvent @event);
    }
}
