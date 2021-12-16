using System.Threading.Tasks;

namespace Module.FeaturesManagementDashboard.Application.Commands
{
    public interface ICommandDispatcher
    {
        ValueTask DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}