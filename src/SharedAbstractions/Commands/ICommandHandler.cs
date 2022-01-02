using System.Threading.Tasks;

namespace SharedAbstractions.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        ValueTask HandleAsync(TCommand command);
    }
}