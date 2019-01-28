using System.Threading.Tasks;

namespace Domain.Core.Handler
{
    public interface IEventHandler<in TEvent>
        where TEvent : Event
    {
        Task Handler(TEvent @event);
    }
}
