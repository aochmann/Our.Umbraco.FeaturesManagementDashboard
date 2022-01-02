using System;
using System.Threading.Tasks;
using FeaturesManagementDashboard.Application.Commands;
using MediatR;

namespace FeaturesManagementDashboard.Infrastructure.HandlerDispatchers
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IPublisher _commandPublisher;

        public CommandDispatcher(IPublisher commandPublisher)
            => _commandPublisher = commandPublisher;

        public async ValueTask DispatchAsync<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            try
            {
                await _commandPublisher.Publish(command);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}