namespace PabloDispatch.Api.Commands;

public interface ICommandPipelineHandler<in TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}