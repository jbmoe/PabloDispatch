using Microsoft.Extensions.DependencyInjection;
using PabloDispatch.Api.Queries;

namespace PabloDispatch.Configuration;

internal class QueryPipeline<TRequest, TResult> : IQueryPipeline<TRequest, TResult>
    where TRequest : IQuery<TResult>
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

    public IQueryPipeline<TRequest, TResult> AddPreProcessor<TRequestPipelineHandler>(ServiceLifetime lifetime)
        where TRequestPipelineHandler : IQueryPipelineHandler<TRequest, TResult>
    {
        var serviceDescription = ServiceDescriptor.Describe(typeof(IQueryPipelineHandler<TRequest, TResult>), typeof(TRequestPipelineHandler), lifetime);
        _preProcessors.Add(serviceDescription);
        return this;
    }

    public IQueryPipeline<TRequest, TResult> AddPostProcessor<TRequestPipelineHandler>(ServiceLifetime lifetime)
        where TRequestPipelineHandler : IQueryPipelineHandler<TRequest, TResult>
    {
        var serviceDescription = ServiceDescriptor.Describe(typeof(IQueryPipelineHandler<TRequest, TResult>), typeof(TRequestPipelineHandler), lifetime);
        _postProcessors.Add(serviceDescription);
        return this;
    }
}