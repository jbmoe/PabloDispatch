using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Exceptions;
using PabloDispatch.Api.Options;
using PabloDispatch.Api.Providers;
using PabloDispatch.Api.Queries;

namespace PabloDispatch.Domain.Services;

/// <summary>
/// Partial dispatcher class for query dispatching.
/// </summary>
public partial class Dispatcher
{
    public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery
        where TResult : class
    {
        var queryHandler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>() ?? throw QueryHandlerNotFoundException.FromTypes<TQuery, TResult>();
        var pipelineProvider = _serviceProvider.GetService<IQueryPipelineProvider<TQuery, TResult>>() ?? throw QueryPipelineProviderNotFoundException.FromTypes<TQuery, TResult>();
        var queryOptionsProvider = _serviceProvider.GetService<IQueryOptionsProvider<TQuery, TResult>>() ?? throw QueryOptionsProviderNotFoundException.FromTypes<TQuery, TResult>();

        var cacheOptions = queryOptionsProvider.CacheOptions;
        if (cacheOptions.EnableCache)
        {
            if (!cacheOptions.CachedPipelines.HasFlag(CachedPipelines.PreProcessors))
            {
                await DispatchQueryPipeline<TQuery, TResult>(query, pipelineProvider.PreProcessors, cancellationToken);
            }

            var cachedResult = await _cacheService.GetCachedValue(
                cacheOptions.CacheKeyFactory(query),
                async () =>
                {
                    if (cacheOptions.CachedPipelines.HasFlag(CachedPipelines.PreProcessors))
                    {
                        await DispatchQueryPipeline<TQuery, TResult>(query, pipelineProvider.PreProcessors, cancellationToken);
                    }

                    var result = await queryHandler.HandleAsync(query, cancellationToken);

                    if (cacheOptions.CachedPipelines.HasFlag(CachedPipelines.PostProcessors))
                    {
                        await DispatchQueryPipeline<TQuery, TResult>(query, pipelineProvider.PostProcessors, cancellationToken);
                    }

                    return result;
                });

            if (!cacheOptions.CachedPipelines.HasFlag(CachedPipelines.PostProcessors))
            {
                await DispatchQueryPipeline<TQuery, TResult>(query, pipelineProvider.PostProcessors, cancellationToken);
            }

            return cachedResult;
        }

        await DispatchQueryPipeline<TQuery, TResult>(query, pipelineProvider.PreProcessors, cancellationToken);

        var result = await queryHandler.HandleAsync(query, cancellationToken);

        await DispatchQueryPipeline<TQuery, TResult>(query, pipelineProvider.PostProcessors, cancellationToken);

        return result;
    }

    private async Task DispatchQueryPipeline<TQuery, TResult>(TQuery query, IReadOnlyList<Type> pipelineProcessorTypes, CancellationToken cancellationToken)
        where TQuery : IQuery
    {
        foreach (var pipelineProcessorType in pipelineProcessorTypes)
        {
            if (_serviceProvider.GetService(pipelineProcessorType) is not IQueryPipelineHandler<TQuery> pipelineProcessor)
            {
                throw QueryPipelineHandlerNotFoundException.FromTypes<TQuery, TResult>();
            }

            await pipelineProcessor.HandleAsync(query, cancellationToken);
        }
    }
}