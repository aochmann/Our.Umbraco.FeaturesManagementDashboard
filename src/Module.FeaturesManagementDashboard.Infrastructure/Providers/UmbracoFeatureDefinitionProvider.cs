using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.FeatureManagement;
using Module.FeaturesManagementDashboard.Application.DI;
using Module.FeaturesManagementDashboard.Domain.Entities.Features;
using Module.FeaturesManagementDashboard.Domain.Repositories;

namespace Module.FeaturesManagementDashboard.Infrastructure.Providers
{
    internal class UmbracoFeatureDefinitionProvider : IFeatureDefinitionProvider
    {
        private readonly IUmbracoFeatureRepository _featureRepository;

        public UmbracoFeatureDefinitionProvider(ICompositionRoot compositionRoot)
            => _featureRepository = compositionRoot.Resolve<IUmbracoFeatureRepository>();

        public async IAsyncEnumerable<FeatureDefinition> GetAllFeatureDefinitionsAsync()
        {
            var features = (await _featureRepository.GetAllAsync())
                .Select(MapTo)
                .ToArray();

            foreach (var feature in features)
            {
                yield return feature;
            }
        }

        public async Task<FeatureDefinition> GetFeatureDefinitionAsync(string featureName)
        {
            var feature = await _featureRepository.GetAsync(new FeatureId(featureName));

            return feature is not null
                ? MapTo(feature)
                : null;
        }

        private static FeatureDefinition MapTo(Feature feature)
            => new()
            {
                Name = feature.Name,
                EnabledFor = feature.Status
                        ? new[]
                            {
                                new FeatureFilterConfiguration
                                {
                                    Name = "AlwaysOn",
                                }
                            }
                        : Array.Empty<FeatureFilterConfiguration>()
            };
    }
}