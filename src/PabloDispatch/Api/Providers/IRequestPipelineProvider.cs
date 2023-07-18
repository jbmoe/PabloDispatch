using PabloDispatch.Api.Requests;

namespace PabloDispatch.Api.Providers;

/// <summary>
/// Provides the pre- and post-processors for the request of type <typeparamref name="TRequest"/>.
/// </summary>
/// <typeparam name="TRequest">The request type the pipeline is tied to.</typeparam>
public interface IRequestPipelineProvider<TRequest>
    where TRequest : IRequest
{
    /// <summary>
    /// Gets a list of the pipeline handlers for pre-processing a request of type <typeparamref name="TRequest"/>.
    /// </summary>
    IList<Type> PreProcessors { get; }

    /// <summary>
    /// Gets a list of the pipeline handlers for post-processing a request of type <typeparamref name="TRequest"/>.
    /// </summary>
    IList<Type> PostProcessors { get; }
}

/// <summary>
/// Provides the pre- and post-processors for the request of type <typeparamref name="TRequest"/> with a result of type <typeparamref name="TResult"></typeparamref>.
/// </summary>
/// <typeparam name="TRequest">The request type the pipeline is tied to.</typeparam>
/// <typeparam name="TResult">The result type the pipeline is tied to.</typeparam>
public interface IRequestPipelineProvider<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    /// <summary>
    /// Gets a list of the pipeline handlers for pre-processing a request of type <typeparamref name="TRequest"/> with a result of type <typeparamref name="TResult"></typeparamref>.
    /// </summary>
    IList<Type> PreProcessors { get; }

    /// <summary>
    /// Gets a list of the pipeline handlers for post-processing a request of type <typeparamref name="TRequest"/> with a result of type <typeparamref name="TResult"></typeparamref>.
    /// </summary>
    IList<Type> PostProcessors { get; }
}