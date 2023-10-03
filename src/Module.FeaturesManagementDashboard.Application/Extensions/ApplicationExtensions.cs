namespace FeaturesManagementDashboard.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            _ = serviceCollection.AddCommands(typeof(ICommandHandler<>));
            return serviceCollection;
        }
    }
}