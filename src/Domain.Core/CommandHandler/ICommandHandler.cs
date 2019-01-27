using Domain.Core.Command;
using System.Threading.Tasks;

namespace Domain.Core.CommandHandler
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
