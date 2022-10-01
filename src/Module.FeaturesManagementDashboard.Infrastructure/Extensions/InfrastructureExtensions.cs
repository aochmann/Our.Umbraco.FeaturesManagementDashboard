using FeaturesManagementDashboard.Application.Commands;
using FeaturesManagementDashboard.Application.Queries;
using FeaturesManagementDashboard.Infrastructure.HandlerDispatchers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using Shared.Domain;
using Shared.Queries;
using SharedAbstractions.DI;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.DependencyInjection;

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

            return registry.AddInfrastructure(umbracoBuilder.Config);
        }

        internal static IServiceCollection AddInfrastructure(this IServiceCollection registry, IConfiguration configuration)
        {
            _ = registry.AddSingleton<IConfiguration>(configuration);

            _ = registry.AddSingleton<ConnectionStrings>(serviceContext => serviceContext
                    .GetService<IConfiguration>()
                    .GetOptions<ConnectionStrings>(
                        "ConnectionStrings",
                        true));

            _ = registry.AddSingleton<IDependencyResolver, CompositionRoot>();

            _ = registry.AddScoped<Mediator>();
            _ = registry.AddScoped<IMediator>(provider => provider.GetRequiredService<Mediator>());
            _ = registry.AddScoped<ISender>(provider => provider.GetRequiredService<Mediator>());
            _ = registry.AddScoped<IPublisher>(provider => provider.GetRequiredService<Mediator>());
            _ = registry.AddScoped<ServiceFactory>(provider => provider.GetRequiredService);

            _ = registry.AddScoped<ICommandDispatcher, CommandDispatcher>();
            _ = registry.AddScoped<IQueryDispatcher, QueryDispatcher>();

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