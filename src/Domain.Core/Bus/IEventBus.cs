using Domain.Core.Event;
using Domain.Core.Handler;
using System.Threading.Tasks;

namespace Domain.Core.Bus
{
    public interface IEventBus
    {
        Task Publish(IEvent @event);

        void Subscribe<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : IEventHandler<TEvent>;
    }
}
