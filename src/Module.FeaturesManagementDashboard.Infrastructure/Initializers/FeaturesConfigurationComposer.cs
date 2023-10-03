namespace FeaturesManagementDashboard.Infrastructure.Initializers;
#pragma warning disable SA1402 // File may only contain a single type

internal class FeaturesConfigurationComposer : ComponentComposer<FeaturesConfigurationComponent>
{
    public override void Compose(IUmbracoBuilder builder)
    {
        var configuration = builder.Config;

        var settings = configuration.GetFeaturesManagementDashboardSettings();

        if (settings is null || !settings.Enabled)
        {
            return;
        }

        _ = builder.Components()
            .Append<FeaturesConfigurationComponent>();
    }
}

internal class FeaturesConfigurationComponent : IComponent
{
    private readonly IConfigurationFeatureRepository _configurationFeatureRepository;
    private readonly IUmbracoFeatureRepository _umbracoFeatureRepository;
    private readonly IMigrationPlanExecutor _migrationPlanExecutor;
    private readonly Umbraco.Cms.Infrastructure.Scoping.IScopeProvider _scopeProvider;
    private readonly IKeyValueService _keyValueService;
    private readonly FeaturesManagementDashboardSettings _featureManagementDashboardSettings;

    public FeaturesConfigurationComponent(
        IMigrationPlanExecutor migrationPlanExecutor,
        Umbraco.Cms.Infrastructure.Scoping.IScopeProvider scopeProvider,
        IKeyValueService keyValueService,
        FeaturesManagementDashboardSettings featureManagementDashboardSettings,
        ICompositionRoot compositionRoot)
    {
        _configurationFeatureRepository = compositionRoot.Resolve<IConfigurationFeatureRepository>();
        _umbracoFeatureRepository = compositionRoot.Resolve<IUmbracoFeatureRepository>();

        _migrationPlanExecutor = migrationPlanExecutor;
        _scopeProvider = scopeProvider;
        _keyValueService = keyValueService;
        _featureManagementDashboardSettings = featureManagementDashboardSettings;
    }

    public void Initialize()
    {
        var migrationPlan = new MigrationPlan("FeatureManagementDashboard");

        migrationPlan.From(string.Empty)
            .To<CreateFeatureManagementTableMigration>(nameof(CreateFeatureManagementTableMigration));

        var upgrader = new Upgrader(migrationPlan);

        upgrader.Execute(_migrationPlanExecutor, _scopeProvider, _keyValueService);

        var configurationFeatures = _configurationFeatureRepository.GetAllAsync()
            .GetAwaiter().GetResult();

        var umbracoFeatures = _umbracoFeatureRepository.GetAllAsync()
            .GetAwaiter().GetResult();

        var featuresToAdd = (_featureManagementDashboardSettings.Override is not true
            ? configurationFeatures.Except(
                umbracoFeatures,
                new FeatureComparer())
            : configurationFeatures).ToArray();

        foreach (var featureToAdd in featuresToAdd)
        {
            _umbracoFeatureRepository.SaveAsync(featureToAdd)
                .GetAwaiter().GetResult();
        }
    }

    public void Terminate()
    {
    }
}

#pragma warning restore SA1402 // File may only contain a single type