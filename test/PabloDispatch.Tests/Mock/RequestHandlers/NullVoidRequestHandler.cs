using PabloDispatch.Api.Requests;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestHandlers;

public class NullVoidRequestHandler : IRequestHandler<NullVoidRequest>
{
    public Task HandleAsync(NullVoidRequest voidRequest, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}