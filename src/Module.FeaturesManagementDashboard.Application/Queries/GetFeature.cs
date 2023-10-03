namespace FeaturesManagementDashboard.Application.Queries
{
    public record GetFeature(string FeatureId) : IQuery<GetFeature, FeatureDto>;
}