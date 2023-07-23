using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Queries;

namespace PabloDispatch.Configuration;

/// <summary>
/// An interface for a query pipeline for pre- and post-processing queries of type <typeparamref name="TQuery"/> with a result of type <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="TQuery">The query type to pre- or post-process.</typeparam>
/// <typeparam name="TResult">The result type to pre- or post-process.</typeparam>
public interface IQueryPipeline<out TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    /// <summary>
    /// Gets the added pre-processors for the command.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<ServiceDescriptor> GetPreProcessors();

    /// <summary>
    /// Gets the added post-processors for the command.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<ServiceDescriptor> GetPostProcessors();

    /// <summary>
    /// Adds the query pipeline handler of type <typeparamref name="TQueryPipelineHandler"/> as a pre-processors for the query.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the pre-processor, <see cref="ServiceLifetime.Transient"/> by default.</param>
    /// <typeparam name="TQueryPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    IQueryPipeline<TQuery, TResult> AddPreProcessor<TQueryPipelineHandler>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TQueryPipelineHandler : IQueryPipelineHandler<TQuery, TResult>;

    /// <summary>
    /// Adds the query pipeline handler of type <typeparamref name="TRequestPipelineHandler"/> as a post-processors for the query.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the pre-processor, <see cref="ServiceLifetime.Transient"/> by default.</param>
    /// <typeparam name="TRequestPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    IQueryPipeline<TQuery, TResult> AddPostProcessor<TRequestPipelineHandler>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TRequestPipelineHandler : IQueryPipelineHandler<TQuery, TResult>;
}