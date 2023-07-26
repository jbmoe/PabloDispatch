namespace PabloDispatch.Api.Exceptions;

public class CommandHandlerNotFoundException : Exception
{
    private CommandHandlerNotFoundException(Type requestType)
        : base($"CommandHandler not found for command {requestType}.")
    {
    }

    public static CommandHandlerNotFoundException FromType<TCommand>()
    {
        return new CommandHandlerNotFoundException(typeof(TCommand));
    }
}