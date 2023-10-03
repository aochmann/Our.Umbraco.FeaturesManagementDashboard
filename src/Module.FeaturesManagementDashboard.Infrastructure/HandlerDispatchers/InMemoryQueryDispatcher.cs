namespace FeaturesManagementDashboard.Infrastructure.HandlerDispatchers;

internal class InMemoryQueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryQueryDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async ValueTask<TResult> DispatchAsync<TQuery, TResult>(IQuery<TQuery, TResult> query)
        where TQuery : class, IQuery<TQuery, TResult>
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.HandleAsync((TQuery)query);
    }
}