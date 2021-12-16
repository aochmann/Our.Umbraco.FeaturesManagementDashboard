using System.Collections.Generic;
using System.Threading.Tasks;
using Module.FeaturesManagementDashboard.Domain.Entities.Features;
using SharedAbstractions.Domain;

namespace Module.FeaturesManagementDashboard.Domain.Repositories
{
    public interface IUmbracoFeatureRepository : IRepository<Feature, FeatureId>
    {
        ValueTask<Feature> GetAsync(FeatureId featureId);

        ValueTask<IEnumerable<Feature>> GetAllAsync();

        ValueTask SaveAsync(Feature feature);

        ValueTask<bool> ExistsAsync(FeatureId featureId);
    }
}