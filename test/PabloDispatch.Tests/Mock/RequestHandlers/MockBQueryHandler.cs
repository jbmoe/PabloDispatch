using PabloDispatch.Api.Queries;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestHandlers;

public class MockBQueryHandler : IQueryHandler<MockQuery, MockModelB>
{
    public const string Code = nameof(MockAQueryHandler);
    public Task<MockModelB> HandleAsync(MockQuery query, CancellationToken cancellationToken)
    {
        query.CallBack?.Invoke(Code);
        return Task.FromResult(new MockModelB());
    }
}