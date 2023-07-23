using PabloDispatch.Api.Commands;

namespace PabloDispatch.Domain.Providers;

internal class CommandPipelineProvider<TCommand> : ICommandPipelineProvider<TCommand>
    where TCommand : ICommand
{
    public CommandPipelineProvider(IReadOnlyList<Type> preProcessors, IReadOnlyList<Type> postProcessors)
    {
        PreProcessors = preProcessors;
        PostProcessors = postProcessors;
    }

    public IReadOnlyList<Type> PreProcessors { get; }
    public IReadOnlyList<Type> PostProcessors { get; }
}