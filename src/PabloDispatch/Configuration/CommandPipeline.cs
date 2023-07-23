using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Commands;

namespace PabloDispatch.Configuration;

internal class CommandPipeline<TCommand> : ICommandPipeline<TCommand>
    where TCommand : ICommand
{
    private readonly List<ServiceDescriptor> _preProcessors = new();
    private readonly List<ServiceDescriptor> _postProcessors = new();

    public IReadOnlyList<ServiceDescriptor> GetPreProcessors()
    {
        return _preProcessors.AsReadOnly();
    }

    public IReadOnlyList<ServiceDescriptor> GetPostProcessors()
    {
        return _postProcessors.AsReadOnly();
    }

    public ICommandPipeline<TCommand> AddPreProcessor<TCommandPipelineHandler>(ServiceLifetime lifetime)
        where TCommandPipelineHandler : ICommandPipelineHandler<TCommand>
    {
        var serviceDescription = ServiceDescriptor.Describe(typeof(ICommandPipelineHandler<TCommand>), typeof(TCommandPipelineHandler), lifetime);
        _preProcessors.Add(serviceDescription);
        return this;
    }

    public ICommandPipeline<TCommand> AddPostProcessor<TCommandPipelineHandler>(ServiceLifetime lifetime)
        where TCommandPipelineHandler : ICommandPipelineHandler<TCommand>
    {
        var serviceDescription = ServiceDescriptor.Describe(typeof(ICommandPipelineHandler<TCommand>), typeof(TCommandPipelineHandler), lifetime);
        _postProcessors.Add(serviceDescription);
        return this;
    }
}