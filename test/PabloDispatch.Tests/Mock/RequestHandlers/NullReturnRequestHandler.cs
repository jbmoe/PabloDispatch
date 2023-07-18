using PabloDispatch.Api.Requests;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestHandlers;

public class NullReturnRequestHandler : IRequestHandler<NullReturnRequest, NullReturnRequestReturnModel>
{
    public Task<NullReturnRequestReturnModel> HandleAsync(NullReturnRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new NullReturnRequestReturnModel());
    }
}