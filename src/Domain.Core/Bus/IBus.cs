using Domain.Core.Command;
using System.Threading.Tasks;

namespace Domain.Core.Bus
{
    public interface IBus
    {
        Task Send(ICommand command);
    }
}
