using PabloDispatch.Api.Requests;
using PabloDispatch.Tests.Mock.Models;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestPipelineHandlers;

public class NullReturnRequestFirstPipelineHandler : IRequestPipelineHandler<NullReturnRequest, NullReturnRequestReturnModel>
{
    public Task HandleAsync(NullReturnRequest request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}