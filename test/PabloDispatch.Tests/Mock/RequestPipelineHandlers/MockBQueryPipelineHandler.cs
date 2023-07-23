using PabloDispatch.Api.Queries;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestPipelineHandlers;

public class MockBQueryPipelineHandler : IQueryPipelineHandler<MockQuery, MockModel>
{
    public Task HandleAsync(MockQuery request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}