using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FeaturesManagementDashboard.Application.DI;
using FeaturesManagementDashboard.Domain.Entities.Features;
using FeaturesManagementDashboard.Domain.Repositories;
using FeaturesManagementDashboard.Infrastructure.Settings;
using Microsoft.FeatureManagement;

namespace FeaturesManagementDashboard.Infrastructure.Providers
{
    internal class UmbracoFeatureDefinitionProvider : IFeatureDefinitionProvider
    {
        private readonly IUmbracoFeatureRepository _featureRepository;
        private readonly FeaturesManagementDashboardSettings _settings;

        public UmbracoFeatureDefinitionProvider(ICompositionRoot compositionRoot)
        {
            _featureRepository = compositionRoot.Resolve<IUmbracoFeatureRepository>();
            _settings = compositionRoot.Resolve<FeaturesManagementDashboardSettings>();
        }

        public async IAsyncEnumerable<FeatureDefinition> GetAllFeatureDefinitionsAsync()
        {
            var features = (await _featureRepository.GetAllAsync())
                .Select(feature => MapTo(feature, _settings.Enabled))
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
                ? MapTo(feature, _settings.Enabled)
                : null;
        }

        private static FeatureDefinition MapTo(Feature feature, bool dashboardEnabled)
            => new()
            {
                Name = feature.Name,
                EnabledFor = !dashboardEnabled || feature.Status
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