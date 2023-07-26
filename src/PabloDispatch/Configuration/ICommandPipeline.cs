using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Commands;

namespace PabloDispatch.Configuration;

/// <summary>
/// A command pipeline for adding pre- and post-processing command pipeline handlers of type <see cref="ICommandPipelineHandler{TCommand}"/>.
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
    /// Adds the command pipeline handler of type <typeparamref name="TCommandPipelineHandler"/> as a pre-processors for the <typeparamref name="TCommand"/>.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the pre-processor, <see cref="ServiceLifetime.Transient"/> by default.</param>
    /// <typeparam name="TCommandPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    ICommandPipeline<TCommand> AddPreProcessor<TCommandPipelineHandler>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TCommandPipelineHandler : ICommandPipelineHandler<TCommand>;

    /// <summary>
    /// Adds the command pipeline handler of type <typeparamref name="TCommandPipelineHandler"/> as a post-processors for the <typeparamref name="TCommand"/>.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the pre-processor, <see cref="ServiceLifetime.Transient"/> by default.</param>
    /// <typeparam name="TCommandPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    ICommandPipeline<TCommand> AddPostProcessor<TCommandPipelineHandler>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TCommandPipelineHandler : ICommandPipelineHandler<TCommand>;
}