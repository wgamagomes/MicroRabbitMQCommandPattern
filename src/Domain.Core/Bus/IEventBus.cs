using Domain.Core;
using Domain.Core.Handler;
using System.Threading.Tasks;

namespace Domain.Core.Bus
{
    public interface IEventBus
    {
        Task Publish(Event @event);

        void Subscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>;
    }
}
