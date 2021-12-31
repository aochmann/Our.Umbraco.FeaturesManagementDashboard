using System.Collections.Generic;
using System.Threading.Tasks;
using FeaturesManagementDashboard.Domain.Entities.Features;
using SharedAbstractions.Domain;

namespace FeaturesManagementDashboard.Domain.Repositories
{
    public interface IConfigurationFeatureRepository : IRepository<Feature, FeatureId>
    {
        ValueTask<Feature> GetAsync(FeatureId featureId);

        ValueTask<IEnumerable<Feature>> GetAllAsync();

        ValueTask SaveAsync(Feature feature);
    }
}