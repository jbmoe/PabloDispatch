using PabloDispatch.Api.Queries;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestHandlers;

public class MockQueryHandler : IQueryHandler<MockQuery, MockModel>
{
    public const string Code = nameof(MockQueryHandler);
    public Task<MockModel> HandleAsync(MockQuery query, CancellationToken cancellationToken)
    {
        query.CallBack?.Invoke(Code);
        return Task.FromResult(new MockModel());
    }
}