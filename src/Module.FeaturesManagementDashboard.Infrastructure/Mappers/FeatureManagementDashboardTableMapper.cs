using DapperExtensions.Mapper;
using Module.FeaturesManagementDashboard.Application.DTO.Features;
using Module.FeaturesManagementDashboard.Infrastructure.Constants;

namespace Module.FeaturesManagementDashboard.Infrastructure.Mappers
{
    internal class FeatureManagementDashboardTableMapper : ClassMapper<FeatureDto>
    {
        public FeatureManagementDashboardTableMapper()
        {
            Table(FeatureManagementConstants.TableName);

            AutoMap();
        }
    }
}