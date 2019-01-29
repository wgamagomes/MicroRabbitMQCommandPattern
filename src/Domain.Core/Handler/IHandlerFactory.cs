using System;
using System.Collections.Generic;

namespace Domain.Core.Handler
{
    public interface IHandlerFactory<TEvent>
          where TEvent : Event
    {
        Func<IEnumerable<IEventHandler<TEvent>>> GetHandlers { get; }
    }
}
