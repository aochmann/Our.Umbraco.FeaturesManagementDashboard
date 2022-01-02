using DapperExtensions.Mapper;
using FeaturesManagementDashboard.Application.DTO.Features;
using FeaturesManagementDashboard.Infrastructure.Constants;

namespace FeaturesManagementDashboard.Infrastructure.Mappers
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