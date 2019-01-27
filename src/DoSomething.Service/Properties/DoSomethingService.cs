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
        public void Execute()
        {
            //Invoke the bus here
        }
    }
}
