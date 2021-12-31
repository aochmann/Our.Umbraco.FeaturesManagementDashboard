using System.Diagnostics.CodeAnalysis;
using FeaturesManagementDashboard.Application.Commands;
using Lamar;
using Shared.Commands;

namespace FeaturesManagementDashboard.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static ServiceRegistry AddApplication([NotNull] this ServiceRegistry registry)
        {
            _ = registry.AddCommands(typeof(ICommandHandler<>));

            return registry;
        }
    }
}