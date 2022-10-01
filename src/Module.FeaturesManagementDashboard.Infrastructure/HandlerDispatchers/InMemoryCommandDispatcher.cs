using System;
using System.Threading.Tasks;
using FeaturesManagementDashboard.Application.Commands;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Services;

namespace FeaturesManagementDashboard.Infrastructure.HandlerDispatchers
{
    internal class InMemoryCommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryCommandDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async ValueTask DispatchAsync<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
        }
    }
}