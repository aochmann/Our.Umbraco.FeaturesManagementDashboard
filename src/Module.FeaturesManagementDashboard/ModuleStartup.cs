namespace FeaturesManagementDashboard
{
    internal class ModuleStartup
    {
        private readonly IUmbracoBuilder _umbracoBuilder;

        public ModuleStartup(IUmbracoBuilder umbracoBuilder)
            => _umbracoBuilder = umbracoBuilder;

        public IUmbracoBuilder Build()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddApplication()
                .AddInfrastructure(_umbracoBuilder);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _ = _umbracoBuilder.Services
                .AddSingleton<ICompositionRoot>(new CompositionRoot(serviceProvider));

            return _umbracoBuilder;
        }
    }
}