﻿using PabloDispatch.Api.Commands;
using PabloDispatch.Api.Queries;
using PabloDispatch.Api.Services;

namespace PabloDispatch.Tests.Mock.Services;

public class NullPabloDispatcher : IPabloDispatcher
{
    public Task<TResult> DispatchAsync<TRequest, TResult>(TRequest query, CancellationToken cancellationToken = default)
        where TRequest : IQuery<TResult>
    {
        throw new NotImplementedException();
    }

    public Task DispatchAsync<TRequest>(TRequest command, CancellationToken cancellationToken = default)
        where TRequest : ICommand
    {
        throw new NotImplementedException();
    }
}