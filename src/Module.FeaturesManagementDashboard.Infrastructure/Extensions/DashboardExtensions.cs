using System.Diagnostics.CodeAnalysis;
using FeaturesManagementDashboard.Infrastructure.Dashboards;
using FeaturesManagementDashboard.Infrastructure.Dashboards.CollectionBuilders;
using Umbraco.Cms.Core.DependencyInjection;

namespace FeaturesManagementDashboard.Infrastructure.Extensions
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