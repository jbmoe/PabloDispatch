using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Commands;
using PabloDispatch.Api.Exceptions;
using PabloDispatch.Api.Queries;
using PabloDispatch.Api.Services;

namespace PabloDispatch.Domain.Services;

public class PabloDispatcher : IPabloDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public PabloDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResult>
    {
        var queryHandler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();
        if (queryHandler == null)
        {
            throw QueryHandlerNotFoundException.FromTypes<TQuery, TResult>();
        }

        var pipelineProvider = _serviceProvider.GetService<IQueryPipelineProvider<TQuery, TResult>>();
        if (pipelineProvider == null)
        {
            throw QueryPipelineProviderNotFoundException.FromTypes<TQuery, TResult>();
        }

        foreach (var preProcessorType in pipelineProvider.PreProcessors)
        {
            if (_serviceProvider.GetService(preProcessorType) is not IQueryPipelineHandler<TQuery, TResult> preProcessor)
            {
                throw QueryPipelineHandlerNotFoundException.FromTypes<TQuery, TResult>();
            }

            await preProcessor.HandleAsync(query, cancellationToken);
        }

        var result = await queryHandler.HandleAsync(query, cancellationToken);

        foreach (var postProcessorType in pipelineProvider.PostProcessors)
        {
            if (_serviceProvider.GetService(postProcessorType) is not IQueryPipelineHandler<TQuery, TResult> postProcessor)
            {
                throw QueryPipelineHandlerNotFoundException.FromTypes<TQuery, TResult>();
            }

            await postProcessor.HandleAsync(query, cancellationToken);
        }

        return result;
    }

    public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();
        if (commandHandler == null)
        {
            throw CommandHandlerNotFoundException.FromType<TCommand>();
        }

        var pipelineProvider = _serviceProvider.GetService<ICommandPipelineProvider<TCommand>>();
        if (pipelineProvider == null)
        {
            throw CommandPipelineProviderNotFoundException.FromType<TCommand>();
        }

        foreach (var preProcessorType in pipelineProvider.PreProcessors)
        {
            if (_serviceProvider.GetService(preProcessorType) is not ICommandPipelineHandler<TCommand> preProcessor)
            {
                throw CommandPipelineHandlerNotFoundException.FromType<TCommand>();
            }

            await preProcessor.HandleAsync(command, cancellationToken);
        }

        await commandHandler.HandleAsync(command, cancellationToken);

        foreach (var postProcessorType in pipelineProvider.PostProcessors)
        {
            if (_serviceProvider.GetService(postProcessorType) is not ICommandPipelineHandler<TCommand> postProcessor)
            {
                throw CommandPipelineHandlerNotFoundException.FromType<TCommand>();
            }

            await postProcessor.HandleAsync(command, cancellationToken);
        }
    }
}