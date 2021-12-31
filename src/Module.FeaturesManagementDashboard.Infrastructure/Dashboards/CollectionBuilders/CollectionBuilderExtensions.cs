using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Cms.Core.DependencyInjection;

namespace FeaturesManagementDashboard.Infrastructure.Dashboards.CollectionBuilders
{
    internal static class CollectionBuilderExtensions
    {
        public static FeatureManagementDashboardCollectionBuilder WithFeatureManagementDashboard(this IUmbracoBuilder builder)
        {
            var featureManagementDashboardCollectionBuilder = builder.WithCollectionBuilder<FeatureManagementDashboardCollectionBuilder>();

            var dashboardCollectionBuilder = builder.Dashboards();

            builder.Services.AddSingleton(dashboardCollectionBuilder);

            dashboardCollectionBuilder.Remove<FeatureManagementDashboard>();

            return featureManagementDashboardCollectionBuilder;
        }
    }
}