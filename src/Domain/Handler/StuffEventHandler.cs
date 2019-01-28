using Domain.Core.Event;
using Domain.Core.Handler;
using System;
using System.Threading.Tasks;

namespace Domain.Handler
{
    public class StuffEventHandler : IEventHandler<IEvent>
    {
        public Task Handler(IEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
