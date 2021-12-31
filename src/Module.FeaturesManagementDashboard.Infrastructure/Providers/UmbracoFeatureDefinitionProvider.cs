using System.Diagnostics.CodeAnalysis;
using FeaturesManagementDashboard.Application.DI;
using FeaturesManagementDashboard.Domain.Entities.Features;
using FeaturesManagementDashboard.Domain.Repositories;
using Microsoft.FeatureManagement;

namespace FeaturesManagementDashboard.Infrastructure.Providers
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

        public async Task<FeatureDefinition> GetFeatureDefinitionAsync([DisallowNull] string featureName)
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