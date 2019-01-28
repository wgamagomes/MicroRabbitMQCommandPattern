using Domain.Core.Event;
using System.Threading.Tasks;

namespace Domain.Core.Bus
{
    public interface IEventBus
    {
        Task Publish(IEvent @event);
    }
}
