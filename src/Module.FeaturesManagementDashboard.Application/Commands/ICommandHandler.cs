namespace FeaturesManagementDashboard.Application.Commands
{
    public interface ICommandHandler<in TCommand>
        where TCommand : class, ICommand
    {
        ValueTask HandleAsync(TCommand command);
    }
}