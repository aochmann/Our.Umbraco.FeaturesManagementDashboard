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

                connectionStrings.ConnectionString =
                    Umbraco.Extensions.ConfigurationExtensions.GetUmbracoConnectionString(configuration);
                connectionStrings.ProviderName = providerName;
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