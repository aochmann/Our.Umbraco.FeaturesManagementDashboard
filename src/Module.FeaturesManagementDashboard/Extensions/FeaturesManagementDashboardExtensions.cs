
namespace FeaturesManagementDashboard.Extensions;

public static class FeaturesManagementDashboardExtensions
{
    public static IUmbracoBuilder AddFeaturesManagementDashboard(this IUmbracoBuilder builder)
        => new ModuleStartup(builder)
            .Build();
}