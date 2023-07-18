using PabloDispatch.Api.Exceptions;
using PabloDispatch.Api.Providers;
using PabloDispatch.Api.Requests;
using PabloDispatch.Api.Services;

namespace PabloDispatch.Domain.Services;

public class PabloDispatcher : IPabloDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public PabloDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResult>
    {
        // Resolve request handler
        var handler = _serviceProvider.GetService(typeof(IRequestHandler<TRequest, TResult>)) as IRequestHandler<TRequest, TResult>;
        if (handler == null)
        {
            throw new RequestHandlerNotFoundException(typeof(TRequest), typeof(TResult));
        }

        // Resolve pipeline provider
        var pipelineProvider = _serviceProvider.GetService(typeof(IRequestPipelineProvider<TRequest, TResult>)) as IRequestPipelineProvider<TRequest, TResult>;

        // Handle pre-processing
        if (pipelineProvider != null)
        {
            foreach (var pipelineHandlerType in pipelineProvider.PreProcessors)
            {
                if (_serviceProvider.GetService(pipelineHandlerType) is not IRequestPipelineHandler<TRequest, TResult> pipelineHandler)
                {
                    throw new RequestPipelineHandlerNotFoundException(typeof(TRequest), typeof(TResult));
                }

                await pipelineHandler.HandleAsync(request, cancellationToken);
            }
        }

        var result = await handler.HandleAsync(request, cancellationToken);

        // Handle post-processing
        if (pipelineProvider != null)
        {
            foreach (var pipelineHandlerType in pipelineProvider.PostProcessors)
            {
                var pipelineHandler = _serviceProvider.GetService(pipelineHandlerType) as IRequestPipelineHandler<TRequest, TResult>;
                if (pipelineHandler == null)
                {
                    throw new RequestPipelineHandlerNotFoundException(typeof(TRequest), typeof(TResult));
                }

                await pipelineHandler.HandleAsync(request, cancellationToken);
            }
        }

        return result;
    }

    public async Task DispatchAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest
    {
        // Resolve request handler
        var handler = _serviceProvider.GetService(typeof(IRequestHandler<TRequest>)) as IRequestHandler<TRequest>;
        if (handler == null)
        {
            throw new RequestHandlerNotFoundException(typeof(TRequest));
        }

        // Resolve pipeline provider
        var pipelineProvider = _serviceProvider.GetService(typeof(IRequestPipelineProvider<TRequest>)) as IRequestPipelineProvider<TRequest>;

        // Handle pre-processing
        if (pipelineProvider != null)
        {
            foreach (var pipelineHandlerType in pipelineProvider.PreProcessors)
            {
                var pipelineHandler = _serviceProvider.GetService(pipelineHandlerType) as IRequestPipelineHandler<TRequest>;
                if (pipelineHandler == null)
                {
                    throw new RequestPipelineHandlerNotFoundException(typeof(TRequest));
                }

                await pipelineHandler.HandleAsync(request, cancellationToken);
            }
        }

        await handler.HandleAsync(request, cancellationToken);

        // Handle post-processing
        if (pipelineProvider != null)
        {
            foreach (var pipelineHandlerType in pipelineProvider.PostProcessors)
            {
                var pipelineHandler = _serviceProvider.GetService(pipelineHandlerType) as IRequestPipelineHandler<TRequest>;
                if (pipelineHandler == null)
                {
                    throw new RequestPipelineHandlerNotFoundException(typeof(TRequest));
                }

                await pipelineHandler.HandleAsync(request, cancellationToken);
            }
        }
    }
}