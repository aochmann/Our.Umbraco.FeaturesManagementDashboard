using FeaturesManagementDashboard.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeaturesManagementDashboard.Infrastructure.Extensions
{
#pragma warning disable CS8604 // Possible null reference argument.

    internal static class SettingsExtensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection serviceCollection)
            => serviceCollection.AddSingleton(factory
                => factory.GetService<IConfiguration>()
                        .GetFeaturesManagementDashboardSettings());

        public static FeaturesManagementDashboardSettings GetFeaturesManagementDashboardSettings(this IConfiguration configuration)
            => configuration.GetOptions<FeaturesManagementDashboardSettings>("FeaturesManagementDashboard");
    }

#pragma warning restore CS8604 // Possible null reference argument.
}