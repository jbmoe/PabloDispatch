using PabloDispatch.Api.Queries;

namespace PabloDispatch.Tests.Mock.Requests;

public class MockQuery : IQuery
{
    public Action<string>? CallBack { get; }

    public MockQuery(Action<string>? callBack = null)
    {
        CallBack = callBack;
    }
}