namespace PabloDispatch.Api.Requests;

/// <summary>
/// An interface for a request pipeline handler for pre- and post-processing requests of type <typeparamref name="TRequest"/>.
/// </summary>
/// <typeparam name="TRequest">The request type to handle.</typeparam>
public interface IRequestPipelineHandler<in TRequest>
    where TRequest : IRequest
{
    /// <summary>
    /// Handles request of type <typeparamref name="TRequest"/> async.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">A cancellation token for cancelling async operations.</param>
    /// <returns>Awaitable task for request handling completion.</returns>
    Task HandleAsync(TRequest request, CancellationToken cancellationToken);
}

/// <summary>
/// An interface for a request pipeline handler for pre- and post-processing requests of type <typeparamref name="TRequest"/> with a return type <typeparamref name="TResult"></typeparamref>.
/// </summary>
/// <typeparam name="TRequest">The request type to handle.</typeparam>
/// <typeparam name="TResult">The expected result of the request.</typeparam>
public interface IRequestPipelineHandler<in TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    /// <summary>
    /// Handles requests of type <typeparamref name="TRequest"/> async.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">A cancellation token for cancelling async operations.</param>
    /// <returns>Awaitable task for request handling completion.</returns>
    Task HandleAsync(TRequest request, CancellationToken cancellationToken);
}