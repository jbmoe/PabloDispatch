using PabloDispatch.Api.Requests;

namespace PabloDispatch.Api.Services;

/// <summary>
/// A request dispatcher for dispatching request to the appropriate handler.
/// </summary>
public interface IPabloDispatcher
{
    /// <summary>
    /// Dispatches request of type <typeparamref name="TRequest"/> async with an expected result of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TRequest">The request type to handle.</typeparam>
    /// <typeparam name="TResult">The expected result of the request.</typeparam>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">A cancellation token for cancelling async operations.</param>
    /// <returns>Awaitable task for request handling completion with the result.</returns>
    Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResult>;

    /// <summary>
    /// Dispatches request of type <typeparamref name="TRequest"/> async.
    /// </summary>
    /// <typeparam name="TRequest">The request type to handle.</typeparam>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">A cancellation token for cancelling async operations.</param>
    /// <returns>Awaitable task for request handling completion.</returns>
    Task DispatchAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest;
}