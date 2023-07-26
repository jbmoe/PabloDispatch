namespace PabloDispatch.Api.Queries;

/// <summary>
/// A query pipeline handler for pre- and post-processing queries of type <typeparamref name="TQuery"/>.
/// </summary>
/// <typeparam name="TQuery">The query type to pre- or post-process.</typeparam>
public interface IQueryPipelineHandler<in TQuery>
    where TQuery : IQuery
{
    /// <summary>
    /// Handles pre- or post-processing of queries of type <typeparamref name="TQuery"/>.
    /// </summary>
    /// <param name="query">The query to handle.</param>
    /// <param name="cancellationToken">A cancellation token for cancelling asynchronous operations.</param>
    /// <returns>Awaitable task for handling completion.</returns>
    Task HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}