using Module.FeaturesManagementDashboard.Application.DTO.Features;

namespace Module.FeaturesManagementDashboard.Application.Queries
{
    public record GetFeature(string FeatureId) : IQuery<GetFeature, FeatureDto>;
}