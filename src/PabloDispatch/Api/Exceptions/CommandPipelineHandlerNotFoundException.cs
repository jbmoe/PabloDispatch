namespace PabloDispatch.Api.Exceptions;

public class CommandPipelineHandlerNotFoundException : Exception
{
    private CommandPipelineHandlerNotFoundException(Type commandType)
        : base($"CommandPipelineHandler not found for command {commandType}.")
    {
    }

    public static CommandPipelineHandlerNotFoundException FromType<TCommand>()
    {
        return new CommandPipelineHandlerNotFoundException(typeof(TCommand));
    }
}