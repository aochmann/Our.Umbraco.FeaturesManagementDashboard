using System;
using System.Threading.Tasks;
using FeaturesManagementDashboard.Application.Queries;
using MediatR;

namespace FeaturesManagementDashboard.Infrastructure.HandlerDispatchers
{
    internal class QueryDispatcher : IQueryDispatcher
    {
        private readonly ISender _querySender;

        public QueryDispatcher(ISender querySender)
            => _querySender = querySender;

        public async ValueTask<TResult> QueryAsync<TQuery, TResult>(IQuery<TQuery, TResult> query)
            where TQuery : class, IQuery<TQuery, TResult>
        {
            try
            {
                return await _querySender.Send(query);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}