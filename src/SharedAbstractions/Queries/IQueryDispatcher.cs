﻿using System.Threading.Tasks;

namespace SharedAbstractions.Queries
{
    public interface IQueryDispatcher
    {
        ValueTask<TResult> QueryAsync<TQuery, TResult>(IQuery<TQuery, TResult> query)
            where TQuery : class, IQuery<TQuery, TResult>;
    }
}