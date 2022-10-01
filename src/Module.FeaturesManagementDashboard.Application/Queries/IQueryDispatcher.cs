using System.Threading.Tasks;

namespace FeaturesManagementDashboard.Application.Queries
{
    public interface IQueryDispatcher
    {
        ValueTask<TResult> DispatchAsync<TQuery, TResult>(IQuery<TQuery, TResult> query)
            where TQuery : class, IQuery<TQuery, TResult>;
    }
}