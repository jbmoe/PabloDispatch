namespace PabloDispatch.Api.Exceptions;

public class CommandPipelineProviderNotFoundException : Exception
{
    private CommandPipelineProviderNotFoundException(Type commandType)
        : base($"CommandPipelineProvider not found for command {commandType}.")
    {
    }

    public static CommandPipelineProviderNotFoundException FromType<TCommand>()
    {
        return new CommandPipelineProviderNotFoundException(typeof(TCommand));
    }
}