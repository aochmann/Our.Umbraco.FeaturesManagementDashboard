namespace SharedAbstractions.Commands;

public interface ICommandDispatcher
{
    ValueTask DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
}