using System.Diagnostics.CodeAnalysis;
using FeaturesManagementDashboard.Application.Commands;
using FeaturesManagementDashboard.Application.Queries;
using FeaturesManagementDashboard.Infrastructure;
using FeaturesManagementDashboard.Infrastructure.HandlerDispatchers;
using Lamar;
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
        public static ServiceRegistry AddInfrastructure([NotNull] this ServiceRegistry registry,
            [NotNull] IUmbracoBuilder umbracoBuilder)
        {
            umbracoBuilder.Services
                .AddSettings()
                .AddProviders();

            _ = umbracoBuilder.Services
                .AddFeatureManagement()
                .AddFeatureFilter<PercentageFilter>()
                .AddFeatureFilter<TimeWindowFilter>();

            return registry.AddInfrastructure(umbracoBuilder.Config);
        }

        internal static ServiceRegistry AddInfrastructure([NotNull] this ServiceRegistry registry, IConfiguration configuration)
        {
            _ = registry.For<IConfiguration>()
                .Use(configuration)
                .Singleton();

            _ = registry.For<ConnectionStrings>()
                .Use(serviceContext => serviceContext.GetService<IConfiguration>()
                    .GetOptions<ConnectionStrings>(
                        "ConnectionStrings",
                        true))
                .Singleton();

            _ = registry.For<IDependencyResolver>()
                .Use<CompositionRoot>()
                .Singleton();

            _ = registry.For<IMediator>()
                .Use<Mediator>()
                .Scoped();

            _ = registry.For<ISender>()
                .Use<Mediator>()
                .Scoped();

            _ = registry.For<IPublisher>()
                .Use<Mediator>()
                .Scoped();

            _ = registry.For<ServiceFactory>()
                .Use(ctx => ctx.GetInstance);

            _ = registry.For<ICommandDispatcher>()
                .Use<CommandDispatcher>()
                .Scoped();

            _ = registry.For<IQueryDispatcher>()
                .Use<QueryDispatcher>()
                .Scoped();

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