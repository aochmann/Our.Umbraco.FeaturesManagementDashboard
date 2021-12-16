using System.Diagnostics.CodeAnalysis;
using Module.FeaturesManagementDashboard.Infrastructure.Dashboards;
using Module.FeaturesManagementDashboard.Infrastructure.Dashboards.CollectionBuilders;
using Umbraco.Cms.Core.DependencyInjection;

namespace Module.FeaturesManagementDashboard.Infrastructure.Extensions
{
    internal static class DashboardExtensions
    {
        public static IUmbracoBuilder AddDashboard([NotNull] this IUmbracoBuilder builder)
        {
            _ = builder.WithFeatureManagementDashboard()
                                     .Add<FeatureManagementDashboard>();

            return builder;
        }
    }
}