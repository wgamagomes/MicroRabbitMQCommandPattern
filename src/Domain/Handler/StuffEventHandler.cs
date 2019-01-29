using Domain.Core;
using Domain.Core.Bus;
using Domain.Core.Handler;
using System.Threading.Tasks;

namespace Domain.Handler
{
    public class StuffEventHandler : IEventHandler<StuffEvent>
    {
        private readonly IEventBus _bus;

        public StuffEventHandler(IEventBus bus )
        {
            _bus = bus;
        }
        public Task Handler(StuffEvent @event)
        {
            //Do here your business logic 

            _bus.Publish(@event);

            return Task.CompletedTask;
        }
    }

    
}
