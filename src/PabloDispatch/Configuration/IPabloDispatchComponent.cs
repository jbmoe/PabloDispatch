using PabloDispatch.Api.Requests;
using PabloDispatch.Api.Services;

namespace PabloDispatch.Configuration;

public interface IPabloDispatchComponent
{
    #region Services

    /// <summary>
    /// Sets the request dispatcher implementation used to type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the implementation.</typeparam>
    /// <returns>Returns component for chaining.</returns>
    IPabloDispatchComponent SetPabloDispatcher<T>()
        where T : class, IPabloDispatcher;

    #endregion

    #region Handlers

    /// <summary>
    /// Sets the request handler implementation used to type <typeparamref name="TRequestHandler"/> for handling requests of type <typeparamref name="TRequest"/>.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request to register a handler for.</typeparam>
    /// <typeparam name="TResult">The type of the result to register the handler for.</typeparam>
    /// <typeparam name="TRequestHandler">The type of the request handler implementation.</typeparam>
    /// <param name="pipelineConfig">Configurator action for configuring the requests pipeline.</param>
    /// <returns>Returns component for chaining.</returns>
    IPabloDispatchComponent SetRequestHandler<TRequest, TResult, TRequestHandler>(Action<IRequestPipeline<TRequest, TResult>>? pipelineConfig = null)
        where TRequest : IRequest<TResult>
        where TRequestHandler : class, IRequestHandler<TRequest, TResult>;

    /// <summary>
    /// Sets the request handler implementation used to type <typeparamref name="TRequestHandler"/> for handling requests of type <typeparamref name="TRequest"/>.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request to register a handler for.</typeparam>
    /// <typeparam name="TRequestHandler">The type of the request handler implementation.</typeparam>
    /// <param name="pipelineConfig">Configurator action for configuring the requests pipeline.</param>
    /// <returns>Returns component for chaining.</returns>
    IPabloDispatchComponent SetRequestHandler<TRequest, TRequestHandler>(Action<IRequestPipeline<TRequest>>? pipelineConfig = null)
        where TRequest : IRequest
        where TRequestHandler : class, IRequestHandler<TRequest>;

    #endregion
}