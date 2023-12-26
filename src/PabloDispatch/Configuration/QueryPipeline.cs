using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Options;
using PabloDispatch.Api.Queries;

namespace PabloDispatch.Configuration;

internal class QueryPipeline<TRequest, TResult> : IQueryPipeline<TRequest, TResult>
    where TRequest : IQuery
{
#pragma warning disable SA1401 // Fields should be private - erroneous warning
    internal readonly List<ServiceDescriptor> PreProcessors = new();
    internal readonly List<ServiceDescriptor> PostProcessors = new();
    internal CacheOptions<TRequest> CacheOptions = new();
#pragma warning restore SA1401 // Fields should be private

    public IQueryPipeline<TRequest, TResult> AddPreProcessor<TQueryPipelineHandler>(ServiceLifetime lifetime)
        where TQueryPipelineHandler : IQueryPipelineHandler<TRequest>
    {
        var serviceDescription = ServiceDescriptor.Describe(typeof(TQueryPipelineHandler), typeof(TQueryPipelineHandler), lifetime);
        PreProcessors.Add(serviceDescription);
        return this;
    }

    public IQueryPipeline<TRequest, TResult> AddPostProcessor<TQueryPipelineHandler>(ServiceLifetime lifetime)
        where TQueryPipelineHandler : IQueryPipelineHandler<TRequest>
    {
        var serviceDescription = ServiceDescriptor.Describe(typeof(TQueryPipelineHandler), typeof(TQueryPipelineHandler), lifetime);
        PostProcessors.Add(serviceDescription);
        return this;
    }

    public IQueryPipeline<TRequest, TResult> SetCacheOptions(CacheOptions<TRequest> cacheOptions)
    {
        CacheOptions = cacheOptions;
        return this;
    }
}