namespace FeaturesManagementDashboard.Application.Queries
{
    public record GetFeatures : IQuery<GetFeatures, IEnumerable<FeatureDto>>;
}