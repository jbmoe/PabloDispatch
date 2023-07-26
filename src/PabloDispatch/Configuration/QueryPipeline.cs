using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Queries;

namespace PabloDispatch.Configuration;

internal class QueryPipeline<TRequest, TResult> : IQueryPipeline<TRequest, TResult>
    where TRequest : IQuery
{
    private readonly List<ServiceDescriptor> _preProcessors = new();
    private readonly List<ServiceDescriptor> _postProcessors = new();

    public IReadOnlyList<ServiceDescriptor> GetPreProcessors()
    {
        return _preProcessors.AsReadOnly();
    }

    public IReadOnlyList<ServiceDescriptor> GetPostProcessors()
    {
        return _postProcessors.AsReadOnly();
    }

    public IQueryPipeline<TRequest, TResult> AddPreProcessor<TQueryPipelineHandler>(ServiceLifetime lifetime)
        where TQueryPipelineHandler : IQueryPipelineHandler<TRequest>
    {
        var serviceDescription = ServiceDescriptor.Describe(typeof(TQueryPipelineHandler), typeof(TQueryPipelineHandler), lifetime);
        _preProcessors.Add(serviceDescription);
        return this;
    }

    public IQueryPipeline<TRequest, TResult> AddPostProcessor<TQueryPipelineHandler>(ServiceLifetime lifetime)
        where TQueryPipelineHandler : IQueryPipelineHandler<TRequest>
    {
        var serviceDescription = ServiceDescriptor.Describe(typeof(TQueryPipelineHandler), typeof(TQueryPipelineHandler), lifetime);
        _postProcessors.Add(serviceDescription);
        return this;
    }
}