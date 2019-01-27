using Domain.Command;
using Domain.Core.Bus;
using Service.Core;

namespace DoSomething.Service
{
    public class DoSomethingService : IRoutine
    {
        private readonly IBus _bus;

        public DoSomethingService(IBus bus)
        {
            _bus = bus;
        }
        public async void Execute()
        {
            await  _bus.Send(new StuffCommand { Description = "Stuff description" });            
        }
    }
}
