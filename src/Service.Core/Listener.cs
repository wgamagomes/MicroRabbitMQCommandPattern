using Domain.Core;
using Domain.Core.Bus;
using Domain.Core.Handler;
using Service.Core;
using System;
using System.Collections.Generic;

namespace Service.Core
{
    public class Listener : IListener
    {
        private readonly IEventBus _bus;

        public Listener (IEventBus bus)
        {
            _bus = bus;
        }

        public void Subscribe<TEvent>(Func<IEnumerable<IEventHandler<TEvent>>> eventHandlerFactory) where TEvent : Event
        {
            _bus.Subscribe(eventHandlerFactory);
        }
    }
}
