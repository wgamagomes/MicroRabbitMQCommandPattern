using Domain.Core.Event;
using System.Threading.Tasks;

namespace Domain.Core.Handler
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task Handler(TEvent @event);
    }
}
