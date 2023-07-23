using PabloDispatch.Api.Queries;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestHandlers;

public class MockQueryHandler : IQueryHandler<MockQuery, MockModel>
{
    public Task<MockModel> HandleAsync(MockQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new MockModel());
    }
}