namespace PabloDispatch.Api.Commands;

/// <summary>
/// Provides the pre- and post-processors for the request of type <typeparamref name="TCommand"/>.
/// </summary>
/// <typeparam name="TCommand">The request type the pipeline is tied to.</typeparam>
public interface ICommandPipelineProvider<TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// Gets a list of the pipeline handlers implementations types for pre-processing a request of type <typeparamref name="TCommand"/>.
    /// </summary>
    IReadOnlyList<Type> PreProcessors { get; }

    /// <summary>
    /// Gets a list of the pipeline handlers implementations types for post-processing a request of type <typeparamref name="TCommand"/>.
    /// </summary>
    IReadOnlyList<Type> PostProcessors { get; }
}