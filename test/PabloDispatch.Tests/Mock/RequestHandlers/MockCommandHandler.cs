using PabloDispatch.Api.Commands;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestHandlers;

public class MockCommandHandler : ICommandHandler<MockCommand>
{
    public Task HandleAsync(MockCommand voidRequest, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}