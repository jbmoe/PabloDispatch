using Microsoft.Extensions.DependencyInjection;
using PabloCache.Abstractions.Configuration;
using PabloDispatch.Api.Commands;
using PabloDispatch.Api.Queries;
using PabloDispatch.Api.Services;

namespace PabloDispatch.Configuration;

public interface IPabloDispatchComponent
{
    #region Services

    /// <summary>
    /// Sets the request dispatcher implementation used to type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the pre-processor, <see cref="ServiceLifetime.Transient"/> by default.</param>
    /// <typeparam name="T">The type of the implementation.</typeparam>
    /// <returns>Returns component for chaining.</returns>
    IPabloDispatchComponent SetPabloDispatcher<T>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where T : class, IDispatcher;

    #endregion

    #region Commands

    /// <summary>
    /// Sets the command handler implementation used to type <typeparamref name="TCommandHandler"/> for handling commands of type <typeparamref name="TCommand"/>.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to register a handler for.</typeparam>
    /// <typeparam name="TCommandHandler">The type of the command handler implementation.</typeparam>
    /// <param name="pipelineConfig">Configurator action for configuring the command pipeline.</param>
    /// <returns>Returns component for chaining.</returns>
    IPabloDispatchComponent SetCommandHandler<TCommand, TCommandHandler>(Action<ICommandPipeline<TCommand>>? pipelineConfig = null)
        where TCommand : ICommand
        where TCommandHandler : class, ICommandHandler<TCommand>;

    #endregion

    #region Queries

    /// <summary>
    /// Sets the query handler implementation used to type <typeparamref name="TQueryHandler"/> for handling queries of type <typeparamref name="TQuery"/>.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query to register a handler for.</typeparam>
    /// <typeparam name="TResult">The type of the query to register the handler for.</typeparam>
    /// <typeparam name="TQueryHandler">The type of the query handler implementation.</typeparam>
    /// <param name="pipelineConfig">Configurator action for configuring the query pipeline.</param>
    /// <returns>Returns component for chaining.</returns>
    IPabloDispatchComponent SetQueryHandler<TQuery, TResult, TQueryHandler>(Action<IQueryPipeline<TQuery, TResult>>? pipelineConfig = null)
        where TQuery : IQuery
        where TQueryHandler : class, IQueryHandler<TQuery, TResult>;

    #endregion

    #region PabloCache
    public IPabloDispatchComponent ConfigurePabloCache(Action<IPabloCacheComponent> configure);

    #endregion
}