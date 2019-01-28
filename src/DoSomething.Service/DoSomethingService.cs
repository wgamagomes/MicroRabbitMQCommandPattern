using Domain.Event;
using Domain.Core.Bus;
using Service.Core;

namespace DoSomething.Service
{
    public class DoSomethingService : IRoutine
    {
        private readonly IEventBus _bus;

        public DoSomethingService(IEventBus bus)
        {
            _bus = bus;
        }
        public async void Execute()
        {
            await  _bus.Publish(new StuffEvent { Description = "Stuff description" });            
        }
    }
}
