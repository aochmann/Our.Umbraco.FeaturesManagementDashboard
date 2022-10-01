namespace FeaturesManagementDashboard.Application.Queries
{
    public interface IQuery<in TQuery, out TResult>
        where TQuery : class, IQuery<TQuery, TResult>
    {
    }
}