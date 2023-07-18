using PabloDispatch.Api.Requests;
using PabloDispatch.Api.Services;

namespace PabloDispatch.Tests.Mock.Services;

public class NullPabloDispatcher : IPabloDispatcher
{
    public Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResult>
    {
        throw new NotImplementedException();
    }

    public Task DispatchAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest
    {
        throw new NotImplementedException();
    }
}