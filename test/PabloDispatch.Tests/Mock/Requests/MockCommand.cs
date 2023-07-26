using PabloDispatch.Api.Commands;

namespace PabloDispatch.Tests.Mock.Requests;

public class MockCommand : ICommand
{
    public Action<string>? CallBack { get; }

    public MockCommand(Action<string>? callBack = null)
    {
        CallBack = callBack;
    }
}