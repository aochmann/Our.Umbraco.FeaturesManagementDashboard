using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Module.FeaturesManagementDashboard.Application.DI;
using Module.FeaturesManagementDashboard.Application.Extensions;
using Module.FeaturesManagementDashboard.Infrastructure;
using Module.FeaturesManagementDashboard.Infrastructure.Extensions;
using Umbraco.Cms.Core.DependencyInjection;

namespace Module.FeaturesManagementDashboard
{
    internal class ModuleStartup
    {
        private readonly IUmbracoBuilder _umbracoBuilder;

        public ModuleStartup(IUmbracoBuilder umbracoBuilder)
            => _umbracoBuilder = umbracoBuilder;

        public IUmbracoBuilder Build()
        {
            var container = new Container(registry =>
            {
                registry = registry
                    .AddApplication()
                    .AddInfrastructure(_umbracoBuilder);
            });

            _ = _umbracoBuilder.Services
                .AddSingleton<ICompositionRoot>(new CompositionRoot(container));

            return _umbracoBuilder;
        }
    }
}