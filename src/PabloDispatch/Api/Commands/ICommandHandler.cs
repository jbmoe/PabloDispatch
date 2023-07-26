namespace PabloDispatch.Api.Commands;

/// <summary>
/// A command handler for handling commands of type <typeparamref name="TCommand"/>.
/// </summary>
/// <typeparam name="TCommand">The command to handle.</typeparam>
public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// Handles commands of type <typeparamref name="TCommand"/> async.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">A cancellation token for cancelling asynchronous operations.</param>
    /// <returns>Awaitable task for handling completion.</returns>
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}