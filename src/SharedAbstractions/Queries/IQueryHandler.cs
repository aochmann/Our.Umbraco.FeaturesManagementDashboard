namespace SharedAbstractions.Queries
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : class, IQuery<TQuery, TResult>
    {
        ValueTask<TResult> HandleAsync(TQuery query);
    }
}