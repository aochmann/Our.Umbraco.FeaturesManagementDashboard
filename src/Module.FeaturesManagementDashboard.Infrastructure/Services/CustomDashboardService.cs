using System.Collections.Generic;
using System.Linq;
using FeaturesManagementDashboard.Infrastructure.Dashboards;
using FeaturesManagementDashboard.Infrastructure.Settings;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Services;

namespace FeaturesManagementDashboard.Infrastructure.Services
{
    public class CustomDashboardService : DashboardService, IDashboardService
    {
        private readonly FeaturesManagementDashboardSettings _featuresManagementDashboardSettings;

        public CustomDashboardService(ISectionService sectionService, DashboardCollection dashboardCollection, ILocalizedTextService localizedText, FeaturesManagementDashboardSettings featuresManagementDashboardSettings) : base(sectionService, dashboardCollection, localizedText)
        {
            _featuresManagementDashboardSettings = featuresManagementDashboardSettings;
        }

        IEnumerable<Tab<IDashboard>> IDashboardService.GetDashboards(string section, IUser currentUser)
        {
#pragma warning disable SA1100 // Do not prefix calls with base unless local implementation exists
            var dashboards = base.GetDashboards(section, currentUser);
#pragma warning restore SA1100 // Do not prefix calls with base unless local implementation exists

            if (section.Equals(Umbraco.Cms.Core.Constants.Applications.Settings)
                    && !_featuresManagementDashboardSettings.Enabled)
            {
                dashboards = dashboards
                    .Where(dashboard => !dashboard.Alias.Equals(FeatureManagementDashboard.DashboardAlias))
                    .ToArray();
            }

            return dashboards;
        }

        IDictionary<string, IEnumerable<Tab<IDashboard>>> IDashboardService.GetDashboards(IUser currentUser)
        {
#pragma warning disable SA1100 // Do not prefix calls with base unless local implementation exists
            var dashboards = base.GetDashboards(currentUser);
#pragma warning restore SA1100 // Do not prefix calls with base unless local implementation exists

            if (!_featuresManagementDashboardSettings.Enabled)
            {
                var settingsDashboards = dashboards[Umbraco.Cms.Core.Constants.Applications.Settings]
                    .Where(dashboard => !dashboard.Alias.Equals(FeatureManagementDashboard.DashboardAlias))
                    .ToArray();

                dashboards[Umbraco.Cms.Core.Constants.Applications.Settings] = settingsDashboards;
            }

            return dashboards;
        }
    }
}