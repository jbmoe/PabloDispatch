using PabloDispatch.Api.Queries;

namespace PabloDispatch.Api.Providers;

/// <summary>
/// Provides the pre- and post-processors for the request of type <typeparamref name="TQuery"/> with a result of type <typeparamref name="TResult"></typeparamref>.
/// </summary>
/// <typeparam name="TQuery">The request type the pipeline is tied to.</typeparam>
/// <typeparam name="TResult">The result type the pipeline is tied to.</typeparam>
public interface IQueryPipelineProvider<TQuery, TResult>
    where TQuery : IQuery
{
    /// <summary>
    /// Gets a list of the pipeline handlers implementations types for pre-processing a request of type <typeparamref name="TQuery"/> with a result of type <typeparamref name="TResult"></typeparamref>.
    /// </summary>
    IReadOnlyList<Type> PreProcessors { get; }

    /// <summary>
    /// Gets a list of the pipeline handlers implementations types for post-processing a request of type <typeparamref name="TQuery"/> with a result of type <typeparamref name="TResult"></typeparamref>.
    /// </summary>
    IReadOnlyList<Type> PostProcessors { get; }
}