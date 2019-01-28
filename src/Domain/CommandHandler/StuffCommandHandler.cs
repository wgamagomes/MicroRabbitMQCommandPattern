using Domain.Event;
using Domain.Core.Event;
using Domain.Core.Handler;
using System;
using System.Threading.Tasks;

namespace Domain.CommandHandler
{
    public class StuffCommandHandler : IEventHandler<IEvent>
    {
        public Task Handler(IEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
