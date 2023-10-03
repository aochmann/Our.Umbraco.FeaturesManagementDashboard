namespace FeaturesManagementDashboard.Infrastructure.Extensions
{
    internal static class ProvidersExtensions
    {
        public static IServiceCollection AddProviders(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<IFeatureDefinitionProvider, UmbracoFeatureDefinitionProvider>();
    }
}