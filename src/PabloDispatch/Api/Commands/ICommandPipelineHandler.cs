namespace PabloDispatch.Api.Commands;

/// <summary>
/// A command pipeline handler for pre- and post-processing commands of type <typeparamref name="TCommand"/>.
/// </summary>
/// <typeparam name="TCommand">The command type to pre- or post-process.</typeparam>
public interface ICommandPipelineHandler<in TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// Handles pre- or post-processing of commands of type <typeparamref name="TCommand"/> async.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">A cancellation token for cancelling asynchronous operations.</param>
    /// <returns>Awaitable task for handling completion.</returns>
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}