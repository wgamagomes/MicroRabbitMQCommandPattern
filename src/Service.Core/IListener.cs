using Domain.Core;
using Domain.Core.Handler;
using System;
using System.Collections.Generic;

namespace Service.Core
{
    public interface IListener
    {
        void Subscribe<TEvent>(Func<IEnumerable<IEventHandler<TEvent>>> eventHandlerFactory)
             where TEvent : Event;


    }
}
