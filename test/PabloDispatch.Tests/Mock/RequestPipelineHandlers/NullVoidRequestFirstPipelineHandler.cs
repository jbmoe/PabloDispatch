﻿using PabloDispatch.Api.Requests;
using PabloDispatch.Tests.Mock.Requests;

namespace PabloDispatch.Tests.Mock.RequestPipelineHandlers;

public class NullVoidRequestFirstPipelineHandler : IRequestPipelineHandler<NullVoidRequest>
{
    public Task HandleAsync(NullVoidRequest request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}