using Microsoft.Extensions.DependencyInjection;
using PabloCache.Abstractions.Configuration;
using PabloDispatch.Api.Commands;
using PabloDispatch.Api.Providers;
using PabloDispatch.Api.Queries;
using PabloDispatch.Api.Services;
using PabloDispatch.Domain.PabloCache.Services;
using PabloDispatch.Domain.Providers;
using PabloDispatch.Domain.Services;

namespace PabloDispatch.Configuration;

internal class PabloDispatchComponent : IPabloDispatchComponent
{
    #region Services

    internal ServiceDescriptor PabloDispatcher { get; private set; } = ServiceDescriptor.Transient<IDispatcher, Dispatcher>();

    public IPabloDispatchComponent SetPabloDispatcher<T>(ServiceLifetime lifetime)
        where T : class, IDispatcher
    {
        PabloDispatcher = ServiceDescriptor.Describe(typeof(IDispatcher), typeof(T), lifetime);
        return this;
    }

    public IReadOnlyList<ServiceDescriptor> GetServices()
    {
        return new List<ServiceDescriptor>
            {
                PabloDispatcher,
            }
            .Union(_queryHandlers)
            .Union(_queryPipelineHandlers)
            .Union(_queryPipelineProviders)
            .Union(_queryOptionsProvider)
            .Union(_commandHandlers)
            .Union(_commandPipelineHandlers)
            .Union(_commandPipelineProviders)
            .ToList();
    }

    #endregion

    #region Commands

    private readonly List<ServiceDescriptor> _commandHandlers = new();

    private readonly List<ServiceDescriptor> _commandPipelineHandlers = new();

    private readonly List<ServiceDescriptor> _commandPipelineProviders = new();

    public IPabloDispatchComponent SetCommandHandler<TCommand, TCommandHandler>(Action<ICommandPipeline<TCommand>>? pipelineConfig = null)
        where TCommand : ICommand
        where TCommandHandler : class, ICommandHandler<TCommand>
    {
        var pipeline = new CommandPipeline<TCommand>();
        pipelineConfig?.Invoke(pipeline);

        var pipelineProvider = new CommandPipelineProvider<TCommand>(
            pipeline.GetPreProcessors().Select(x => x.ImplementationType!).ToList(),
            pipeline.GetPostProcessors().Select(x => x.ImplementationType!).ToList());

        _commandHandlers.Add(ServiceDescriptor.Transient<ICommandHandler<TCommand>, TCommandHandler>());
        _commandPipelineHandlers.AddRange(pipeline.GetPreProcessors());
        _commandPipelineHandlers.AddRange(pipeline.GetPostProcessors());
        _commandPipelineProviders.Add(ServiceDescriptor.Transient<ICommandPipelineProvider<TCommand>>(_ => pipelineProvider));

        return this;
    }

    #endregion

    #region Queries

    private readonly List<ServiceDescriptor> _queryHandlers = new();

    private readonly List<ServiceDescriptor> _queryPipelineHandlers = new();

    private readonly List<ServiceDescriptor> _queryPipelineProviders = new();

    private readonly List<ServiceDescriptor> _queryOptionsProvider = new();

    public IPabloDispatchComponent SetQueryHandler<TQuery, TResult, TQueryHandler>(Action<IQueryPipeline<TQuery, TResult>>? pipelineConfig = null)
        where TQuery : IQuery
        where TResult : class
        where TQueryHandler : class, IQueryHandler<TQuery, TResult>
    {
        var pipeline = new QueryPipeline<TQuery, TResult>();
        pipelineConfig?.Invoke(pipeline);

        var pipelineProvider = new QueryPipelineProvider<TQuery, TResult>(
            pipeline.PreProcessors.Select(x => x.ImplementationType!).ToList(),
            pipeline.PostProcessors.Select(x => x.ImplementationType!).ToList());

        var queryOptionsProvider = new QueryOptionsProvider<TQuery>(pipeline.CacheOptions);

        _queryHandlers.Add(ServiceDescriptor.Transient<IQueryHandler<TQuery, TResult>, TQueryHandler>());
        _queryPipelineHandlers.AddRange(pipeline.PreProcessors);
        _queryPipelineHandlers.AddRange(pipeline.PostProcessors);
        _queryPipelineProviders.Add(ServiceDescriptor.Transient<IQueryPipelineProvider<TQuery, TResult>>(_ => pipelineProvider));
        _queryOptionsProvider.Add(ServiceDescriptor.Transient<IQueryOptionsProvider<TQuery>>(_ => queryOptionsProvider));

        return this;
    }

    #endregion

    #region PabloCache

    internal Action<IPabloCacheComponent> PabloCacheConfig { get; set; } = component => component.SetCacheService<NullCacheService>();

    /// <summary>
    /// Configures PabloCache within the PabloDispatch component.
    /// </summary>
    /// <param name="configure">Configurator action for configuring PabloCache.</param>
    /// <returns>Returns the component for chaining.</returns>
    public IPabloDispatchComponent ConfigurePabloCache(Action<IPabloCacheComponent> configure)
    {
        PabloCacheConfig = configure;
        return this;
    }

    #endregion
}