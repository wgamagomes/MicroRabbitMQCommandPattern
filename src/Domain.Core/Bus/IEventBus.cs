using Domain.Core.Handler;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core.Bus
{
    public interface IEventBus
    {
        Task Publish(Event @event);

        Task Subscribe<TEvent>(Func<IEnumerable<IEventHandler<TEvent>>> eventHandlerFactory)
            where TEvent : Event;

    }
}
