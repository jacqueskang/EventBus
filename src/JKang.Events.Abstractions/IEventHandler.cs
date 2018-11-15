using System.Threading.Tasks;

namespace JKang.Events
{
    public interface IEventHandler<TEvent>
    {
        Task HandleEventAsync(TEvent @event);
    }
}
