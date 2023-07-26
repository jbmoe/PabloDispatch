using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Queries;

namespace PabloDispatch.Configuration;

/// <summary>
/// A query pipeline for adding pre- and post-processing query pipeline handlers of type <see cref="IQueryPipelineHandler{TQuery}"/>.
/// </summary>
/// <typeparam name="TQuery">The query type to pre- or post-process.</typeparam>
/// <typeparam name="TResult">The result type to pre- or post-process.</typeparam>
public interface IQueryPipeline<out TQuery, TResult>
    where TQuery : IQuery
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
    /// Adds the query pipeline handler of type <typeparamref name="TQueryPipelineHandler"/> as a pre-processors for the <typeparamref name="TQuery"/>.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the pre-processor, <see cref="ServiceLifetime.Transient"/> by default.</param>
    /// <typeparam name="TQueryPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    IQueryPipeline<TQuery, TResult> AddPreProcessor<TQueryPipelineHandler>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TQueryPipelineHandler : IQueryPipelineHandler<TQuery>;

    /// <summary>
    /// Adds the query pipeline handler of type <typeparamref name="TRequestPipelineHandler"/> as a post-processors for the <typeparamref name="TQuery"/>.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the pre-processor, <see cref="ServiceLifetime.Transient"/> by default.</param>
    /// <typeparam name="TRequestPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    IQueryPipeline<TQuery, TResult> AddPostProcessor<TRequestPipelineHandler>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TRequestPipelineHandler : IQueryPipelineHandler<TQuery>;
}