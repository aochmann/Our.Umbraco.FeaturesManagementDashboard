namespace FeaturesManagementDashboard.Infrastructure.Dashboards
{
    [Weight(80)]
    public class FeatureManagementDashboard : IDashboard
    {
        public const string DashboardAlias = "Our.Umbraco.FeaturesManagementDashboard";
        public string Alias => DashboardAlias;

        public string[] Sections => new[]
        {
            Umbraco.Cms.Core.Constants.Applications.Settings
        };

        public IAccessRule[] AccessRules => Array.Empty<IAccessRule>();

        public string View => "/App_Plugins/FeatureManagementDashboard/dashboard.html";
    }
}