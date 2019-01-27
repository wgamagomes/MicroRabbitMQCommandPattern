using Domain.Command;
using Domain.Core.Command;
using Domain.Core.CommandHandler;
using System;
using System.Threading.Tasks;

namespace Domain.CommandHandler
{
    public class StuffCommandHandler : ICommandHandler<ICommand>
    {
        public Task Handle(ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}
