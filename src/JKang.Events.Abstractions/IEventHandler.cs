using System.Threading.Tasks;

namespace JKang.Events
{
    public interface IEventHandler<TEvent>
        where TEvent: IEvent
    {
        Task HandleEventAsync(TEvent @event);
    }
}
