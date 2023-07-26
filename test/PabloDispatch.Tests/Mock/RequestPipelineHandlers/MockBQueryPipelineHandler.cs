using PabloDispatch.Api.Queries;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestPipelineHandlers;

public class MockBQueryPipelineHandler : IQueryPipelineHandler<MockQuery, MockModel>
{
    public const string Code = nameof(MockBQueryPipelineHandler);
    public Task HandleAsync(MockQuery request, CancellationToken cancellationToken)
    {
        request.CallBack?.Invoke(Code);
        return Task.CompletedTask;
    }
}