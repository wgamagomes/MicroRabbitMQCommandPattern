using Domain.Core.Handler;
using System.Threading.Tasks;

namespace Domain.Handler
{
    public class StuffEventHandler : IEventHandler<StuffEvent>
    {
        public Task Handler(StuffEvent @event)
        {
            //Do here your business logic 
            return Task.CompletedTask;
        }
    }
}
