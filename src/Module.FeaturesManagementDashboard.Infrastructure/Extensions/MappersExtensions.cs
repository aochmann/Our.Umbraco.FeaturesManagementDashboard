namespace FeaturesManagementDashboard.Infrastructure.Extensions;

internal static class MappersExtensions
{
    public static IServiceCollection AddMappers(this IServiceCollection serviceCollection)
    {
        DapperExtensions.DapperExtensions.SetMappingAssemblies(new[]
        {
            typeof(MappersExtensions).Assembly
        });

        DapperExtensions.DapperAsyncExtensions.SetMappingAssemblies(new[]
        {
            typeof(MappersExtensions).Assembly
        });

        return MappersMap.MappersExtensions.AddMappers(serviceCollection);
    }
}