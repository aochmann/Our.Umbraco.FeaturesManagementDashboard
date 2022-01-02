using FeaturesManagementDashboard.Application.DI;
using FeaturesManagementDashboard.Application.Extensions;
using FeaturesManagementDashboard.Infrastructure;
using FeaturesManagementDashboard.Infrastructure.Extensions;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;

namespace FeaturesManagementDashboard
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