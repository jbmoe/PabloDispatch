using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Commands;
using PabloDispatch.Api.Providers;
using PabloDispatch.Api.Queries;
using PabloDispatch.Api.Services;
using PabloDispatch.Domain.Providers;
using PabloDispatch.Domain.Services;

namespace PabloDispatch.Configuration;

public class PabloDispatchComponent : IPabloDispatchComponent
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

    public IPabloDispatchComponent SetQueryHandler<TQuery, TResult, TQueryHandler>(Action<IQueryPipeline<TQuery, TResult>>? pipelineConfig = null)
        where TQuery : IQuery
        where TQueryHandler : class, IQueryHandler<TQuery, TResult>
    {
        var pipeline = new QueryPipeline<TQuery, TResult>();
        pipelineConfig?.Invoke(pipeline);

        var pipelineProvider = new QueryPipelineProvider<TQuery, TResult>(
            pipeline.GetPreProcessors().Select(x => x.ImplementationType!).ToList(),
            pipeline.GetPostProcessors().Select(x => x.ImplementationType!).ToList());

        _queryHandlers.Add(ServiceDescriptor.Transient<IQueryHandler<TQuery, TResult>, TQueryHandler>());
        _queryPipelineHandlers.AddRange(pipeline.GetPreProcessors());
        _queryPipelineHandlers.AddRange(pipeline.GetPostProcessors());
        _queryPipelineProviders.Add(ServiceDescriptor.Transient<IQueryPipelineProvider<TQuery, TResult>>(_ => pipelineProvider));

        return this;
    }

    #endregion
}