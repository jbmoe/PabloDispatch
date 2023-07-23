using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Commands;

namespace PabloDispatch.Configuration;

/// <summary>
/// An interface for a command pipeline for pre- and post-processing commands of type <typeparamref name="TCommand"/>.
/// </summary>
/// <typeparam name="TCommand">The command type to pre- or post-process.</typeparam>
public interface ICommandPipeline<out TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// Gets the added pre-processors for the command.
    /// </summary>
    /// <returns></returns>
    internal IReadOnlyList<ServiceDescriptor> GetPreProcessors();

    /// <summary>
    /// Gets the added post-processors for the command.
    /// </summary>
    /// <returns></returns>
    internal IReadOnlyList<ServiceDescriptor> GetPostProcessors();

    /// <summary>
    /// Adds the command pipeline handler of type <typeparamref name="TCommandPipelineHandler"/> as a pre-processors for the command.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the pre-processor, <see cref="ServiceLifetime.Transient"/> by default.</param>
    /// <typeparam name="TCommandPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    ICommandPipeline<TCommand> AddPreProcessor<TCommandPipelineHandler>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TCommandPipelineHandler : ICommandPipelineHandler<TCommand>;

    /// <summary>
    /// Adds the command pipeline handler of type <typeparamref name="TCommandPipelineHandler"/> as a post-processors for the command.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the pre-processor, <see cref="ServiceLifetime.Transient"/> by default.</param>
    /// <typeparam name="TCommandPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    ICommandPipeline<TCommand> AddPostProcessor<TCommandPipelineHandler>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TCommandPipelineHandler : ICommandPipelineHandler<TCommand>;
}