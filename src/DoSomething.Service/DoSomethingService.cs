using Domain;
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
            //Do here your business logic and after publish your event
            await _bus.Publish(new StuffEvent { Description = "Stuff description" });            
        }
    }
}
