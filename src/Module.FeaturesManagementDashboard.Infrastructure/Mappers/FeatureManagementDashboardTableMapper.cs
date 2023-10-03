namespace FeaturesManagementDashboard.Infrastructure.Mappers;

internal class FeatureManagementDashboardTableMapper : ClassMapper<FeatureDto>
{
    public FeatureManagementDashboardTableMapper()
    {
        Table(FeatureManagementConstants.TableName);

        AutoMap();
    }
}