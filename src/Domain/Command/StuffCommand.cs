using Domain.Core.Command;

namespace Domain.Command
{
    public abstract class StuffCommand : ICommand
    {
        public abstract bool IsValid();
    }
}
