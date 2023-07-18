using PabloDispatch.Api.Requests;

namespace PabloDispatch.Configuration;

/// <summary>
/// An interface for a request pipeline for pre- and post-processing requests of type <typeparamref name="TRequest"/> with a result of type <typeparamref name="TResult"></typeparamref>.
/// </summary>
/// <typeparam name="TRequest">The request type to pre- or post-process.</typeparam>
/// <typeparam name="TResult">The result type to pre- or post-process.</typeparam>
public interface IRequestPipeline<out TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    /// <summary>
    /// Adds the request pipeline handler of type <typeparamref name="TRequestPipelineHandler"/> as a pre-processors for the request.
    /// </summary>
    /// <typeparam name="TRequestPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    IRequestPipeline<TRequest, TResult> AddPreProcessor<TRequestPipelineHandler>()
        where TRequestPipelineHandler : IRequestPipelineHandler<TRequest, TResult>;

    /// <summary>
    /// Adds the request pipeline handler of type <typeparamref name="TRequestPipelineHandler"/> as a post-processors for the request.
    /// </summary>
    /// <typeparam name="TRequestPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    IRequestPipeline<TRequest, TResult> AddPostProcessor<TRequestPipelineHandler>()
        where TRequestPipelineHandler : IRequestPipelineHandler<TRequest, TResult>;
}

/// <summary>
/// An interface for a request pipeline for pre- and post-processing requests of type <typeparamref name="TRequest"/>.
/// </summary>
/// <typeparam name="TRequest">The request type to pre- or post-process.</typeparam>
public interface IRequestPipeline<out TRequest>
    where TRequest : IRequest
{
    /// <summary>
    /// Adds the request pipeline handler of type <typeparamref name="TRequestPipelineHandler"/> as a pre-processors for the request.
    /// </summary>
    /// <typeparam name="TRequestPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    IRequestPipeline<TRequest> AddPreProcessor<TRequestPipelineHandler>()
        where TRequestPipelineHandler : IRequestPipelineHandler<TRequest>;

    /// <summary>
    /// Adds the request pipeline handler of type <typeparamref name="TRequestPipelineHandler"/> as a post-processors for the request.
    /// </summary>
    /// <typeparam name="TRequestPipelineHandler">The pipeline handler.</typeparam>
    /// <returns>Returns pipeline for chaining.</returns>
    IRequestPipeline<TRequest> AddPostProcessor<TRequestPipelineHandler>()
        where TRequestPipelineHandler : IRequestPipelineHandler<TRequest>;
}