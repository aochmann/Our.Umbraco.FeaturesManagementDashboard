namespace FeaturesManagementDashboard.Domain.Repositories;

public interface IConfigurationFeatureRepository : IRepository<Feature, FeatureId>
{
    ValueTask<Feature> GetAsync(FeatureId featureId);

    ValueTask<IEnumerable<Feature>> GetAllAsync();

    ValueTask SaveAsync(Feature feature);
}