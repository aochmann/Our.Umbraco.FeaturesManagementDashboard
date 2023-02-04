using FeaturesManagementDashboard.Application.Commands;
using FeaturesManagementDashboard.Application.Queries;
using FeaturesManagementDashboard.Infrastructure.HandlerDispatchers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using Shared.Domain;
using Shared.Queries;
using SharedAbstractions.DI;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.DependencyInjection;
using InMemoryQueryDispatcher = FeaturesManagementDashboard.Infrastructure.HandlerDispatchers.InMemoryQueryDispatcher;

namespace FeaturesManagementDashboard.Infrastructure.Extensions
{
#pragma warning disable CS8604 // Possible null reference argument.

    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection registry,
            IUmbracoBuilder umbracoBuilder)
        {
            umbracoBuilder.Services
                .AddSettings()
                .AddProviders();

            _ = umbracoBuilder.Services
                .AddFeatureManagement()
                .AddFeatureFilter<PercentageFilter>()
                .AddFeatureFilter<TimeWindowFilter>();

            _ = umbracoBuilder.AddDashboard();

            _ = registry.AddSingleton<IConfiguration>(umbracoBuilder.Config);
            _ = registry.AddSingleton<ConnectionStrings>(serviceContext =>
            {
                var configuration = serviceContext.GetRequiredService<IConfiguration>();
                var connectionStrings = new ConnectionStrings();

                var connectionStringSection = configuration.GetSection("ConnectionStrings");
                if (connectionStringSection is null)
                {
                    return connectionStrings;
                }

                var umbracoDbDsn = connectionStringSection.GetValue<string>("umbracoDbDSN");
                var providerName = connectionStringSection.GetValue<string>("umbracoDbDSN_ProviderName");
                if (umbracoDbDsn is null)
                {
                    return connectionStrings;
                }

#if NET6_0 || NET7_0
                connectionStrings.ConnectionString =
                    Umbraco.Extensions.ConfigurationExtensions.GetUmbracoConnectionString(configuration);
                connectionStrings.ProviderName = providerName;
#endif
#if NET5_0
                connectionStrings.UmbracoConnectionString = new Umbraco.Cms.Core.Configuration.ConfigConnectionString(
                    Umbraco.Cms.Core.Constants.System.UmbracoConnectionName,
                    umbracoDbDsn);
#endif

                return connectionStrings;
            });

            _ = registry.AddSingleton<IDependencyResolver, CompositionRoot>();
            _ = registry.AddScoped<ICommandDispatcher, InMemoryCommandDispatcher>();
            _ = registry.AddScoped<IQueryDispatcher, InMemoryQueryDispatcher>();

            _ = registry
                .AddSettings()
                .AddServices()
                .AddRepositories()
                .AddMappers()
                .AddQueries(typeof(IQueryHandler<,>));

            return registry;
        }
    }

#pragma warning restore CS8604 // Possible null reference argument.
}