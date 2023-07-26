using PabloDispatch.Api.Queries;
using PabloDispatch.Tests.Mock.Models;

namespace PabloDispatch.Tests.Mock.Requests;

public class MockQuery : IQuery<MockModel>
{
    public Action<string>? CallBack { get; }

    public MockQuery(Action<string>? callBack = null)
    {
        CallBack = callBack;
    }
}