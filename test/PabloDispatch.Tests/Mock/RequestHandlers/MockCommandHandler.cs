using PabloDispatch.Api.Commands;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestHandlers;

public class MockCommandHandler : ICommandHandler<MockCommand>
{
    public const string Code = nameof(MockCommandHandler);
    public Task HandleAsync(MockCommand command, CancellationToken cancellationToken = default)
    {
        command.CallBack?.Invoke(Code);
        return Task.CompletedTask;
    }
}