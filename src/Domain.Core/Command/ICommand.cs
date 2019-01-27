namespace Domain.Core.Command
{
    public interface ICommand
    {
        bool IsValid();
    }
}
