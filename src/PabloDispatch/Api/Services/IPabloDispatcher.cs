using PabloDispatch.Api.Commands;
using PabloDispatch.Api.Queries;

namespace PabloDispatch.Api.Services;

/// <summary>
/// A request dispatcher for dispatching request to the appropriate handler.
/// </summary>
public interface IPabloDispatcher
{
    /// <summary>
    /// Dispatches a query of type <typeparamref name="TQuery"/> asynchronously with an expected result of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TQuery">The query type to handle.</typeparam>
    /// <typeparam name="TResult">The expected result of the request.</typeparam>
    /// <param name="query">The query to handle.</param>
    /// <param name="cancellationToken">A cancellation token for cancelling async operations.</param>
    /// <returns>Awaitable task for query handling completion with the result.</returns>
    Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResult>;

    /// <summary>
    /// Dispatches a command of type <typeparamref name="TCommand"/> asynchronously.
    /// </summary>
    /// <typeparam name="TCommand">The command type to handle.</typeparam>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">A cancellation token for cancelling async operations.</param>
    /// <returns>Awaitable task for command handling completion.</returns>
    Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand;
}