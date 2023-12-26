using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Commands;
using PabloDispatch.Api.Exceptions;
using PabloDispatch.Api.Providers;

namespace PabloDispatch.Domain.Services;

/// <summary>
/// Partial dispatcher class for command dispatching.
/// </summary>
public partial class Dispatcher
{
    public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>() ?? throw CommandHandlerNotFoundException.FromType<TCommand>();
        var pipelineProvider = _serviceProvider.GetService<ICommandPipelineProvider<TCommand>>() ?? throw CommandPipelineProviderNotFoundException.FromType<TCommand>();

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