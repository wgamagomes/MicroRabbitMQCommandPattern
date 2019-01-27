using Domain.Command;
using Domain.Core.CommandHandler;
using System;
using System.Threading.Tasks;

namespace Domain.CommandHandler
{
    public class StuffCommandHandler : ICommandHandler<StuffCommand>
    {
        public Task Handle(StuffCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
