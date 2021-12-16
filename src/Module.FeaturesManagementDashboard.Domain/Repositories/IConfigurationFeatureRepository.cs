﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Module.FeaturesManagementDashboard.Domain.Entities.Features;
using SharedAbstractions.Domain;

namespace Module.FeaturesManagementDashboard.Domain.Repositories
{
    public interface IConfigurationFeatureRepository : IRepository<Feature, FeatureId>
    {
        ValueTask<Feature> GetAsync(FeatureId featureId);

        ValueTask<IEnumerable<Feature>> GetAllAsync();

        ValueTask SaveAsync(Feature feature);
    }
}