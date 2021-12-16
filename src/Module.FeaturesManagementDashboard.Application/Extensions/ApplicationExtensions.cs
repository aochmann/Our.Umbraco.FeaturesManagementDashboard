using System.Diagnostics.CodeAnalysis;
using Lamar;
using Module.FeaturesManagementDashboard.Application.Commands;
using Shared.Commands;

namespace Module.FeaturesManagementDashboard.Application.Extensions
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