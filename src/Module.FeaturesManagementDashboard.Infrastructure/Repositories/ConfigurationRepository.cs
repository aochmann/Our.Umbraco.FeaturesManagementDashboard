namespace FeaturesManagementDashboard.Infrastructure.Repositories;

internal class ConfigurationRepository : IConfigurationFeatureRepository
{
    private readonly IConfigurationSection _featureManagementSection;
    private readonly IConfiguration _configuration;

    public ConfigurationRepository(IConfiguration configuration)
    {
        _configuration = configuration;

        _featureManagementSection = configuration
            .GetSection("FeatureManagement");
    }

    public async ValueTask<IEnumerable<Feature>> GetAllAsync()
        => _featureManagementSection
            .GetChildren()
            .Select(featureSection => GetFeature(featureSection))
            .Where(feature => feature is not null)
            .ToArray();

    public async ValueTask<Feature> GetAsync(FeatureId featureId)
        => _featureManagementSection
            .GetChildren()
            .Select(featureSection => GetFeature(featureSection))
            .Where(feature => feature is not null)
            .FirstOrDefault(feature => feature.Id.Equals(featureId));

    public async ValueTask SaveAsync(Feature feature)
    {
        var featureSection = GetFeatureSection(feature.Name);

        featureSection.Value = feature.Status.ToString();

        var root = _configuration as IConfigurationRoot;

        if (root is null)
        {
            return;
        }

        var providers = root.Providers
            .Where(provider =>
                provider.TryGet(featureSection.Path, out var _))
            .ToArray();

        foreach (var provider in providers)
        {
            provider.Set(featureSection.Path, featureSection.Value);
        }
    }

    private IConfigurationSection GetFeatureSection(string featureName)
        => _featureManagementSection.GetChildren()
            .FirstOrDefault(section => section.Key.Equals(featureName));

    private static Feature GetFeature(IConfigurationSection featureSection)
    {
        var subConfigurations = featureSection.GetChildren();

        if (!subConfigurations?.Any() ?? false)
        {
            var featureName = featureSection.Key;
            var featureStatus = featureSection.Get<bool>();

            return new Feature(
                featureName,
                featureStatus);
        }

        return null;
    }
}