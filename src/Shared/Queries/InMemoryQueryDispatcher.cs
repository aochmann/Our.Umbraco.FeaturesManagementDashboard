﻿using System;
using System.Threading.Tasks;
using Shared.Exceptions;
using SharedAbstractions.DI;
using SharedAbstractions.Queries;

namespace Shared.Queries
{
    public class InMemoryQueryDispatcher : IQueryDispatcher
    {
        private readonly IDependencyResolver _dependencyResolver;

        public InMemoryQueryDispatcher(IDependencyResolver dependencyResolver)
            => _dependencyResolver = dependencyResolver;

        public async ValueTask<TResult> QueryAsync<TQuery, TResult>(IQuery<TQuery, TResult> query)
            where TQuery : class, IQuery<TQuery, TResult>
        {
            try
            {
                var handler = _dependencyResolver.Resolve<IQueryHandler<TQuery, TResult>>();

                return await handler.HandleAsync((TQuery)query);
            }
            catch (DependencyNotFoundException dependencyNotFoundException)
            {
                throw new QueryHandlerNotFoundException(
                    $"Query handler for {typeof(TQuery).Name} not found.",
                    dependencyNotFoundException);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}