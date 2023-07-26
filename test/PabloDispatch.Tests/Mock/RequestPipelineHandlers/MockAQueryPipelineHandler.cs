using PabloDispatch.Api.Queries;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestPipelineHandlers;

public class MockAQueryPipelineHandler : IQueryPipelineHandler<MockQuery, MockModel>
{
    public const string Code = nameof(MockAQueryPipelineHandler);
    public Task HandleAsync(MockQuery request, CancellationToken cancellationToken)
    {
        request.CallBack?.Invoke(Code);
        return Task.CompletedTask;
    }
}