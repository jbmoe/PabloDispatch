using PabloDispatch.Api.Commands;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestPipelineHandlers;

public class MockACommandPipelineHandler : ICommandPipelineHandler<MockCommand>
{
    public const string Code = nameof(MockACommandPipelineHandler);
    public Task HandleAsync(MockCommand request, CancellationToken cancellationToken)
    {
        request.CallBack?.Invoke(Code);
        return Task.CompletedTask;
    }
}