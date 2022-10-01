using FeaturesManagementDashboard.Infrastructure.Services;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace FeaturesManagementDashboard.Infrastructure.Extensions
{
    internal static class DashboardExtensions
    {
        public static IUmbracoBuilder AddDashboard(this IUmbracoBuilder builder)
        {
            builder.Services.AddUnique<IDashboardService, CustomDashboardService>();

            return builder;
        }
    }
}