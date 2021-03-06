using MediatR;

namespace FeaturesManagementDashboard.Application.Queries
{
    public interface IQuery<in TQuery, out TResult> : IRequest<TResult>
        where TQuery : class, IQuery<TQuery, TResult>
    {
    }
}