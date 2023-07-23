using PabloDispatch.Api.Commands;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestPipelineHandlers;

public class MockACommandPipelineHandler : ICommandPipelineHandler<MockCommand>
{
    public Task HandleAsync(MockCommand request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}