using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Module.FeaturesManagementDashboard.Infrastructure.Providers;

namespace Module.FeaturesManagementDashboard.Infrastructure.Extensions
{
    internal static class ProvidersExtensions
    {
        public static IServiceCollection AddProviders(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<IFeatureDefinitionProvider, UmbracoFeatureDefinitionProvider>();
    }
}