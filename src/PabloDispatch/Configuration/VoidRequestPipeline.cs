using PabloDispatch.Api.Requests;

namespace PabloDispatch.Configuration;

internal class VoidRequestPipeline<TRequest> : IRequestPipeline<TRequest>
    where TRequest : IRequest
{
    private readonly IList<Type> _preProcessors = new List<Type>();
    private readonly IList<Type> _postProcessors = new List<Type>();

    public IList<Type> GetPreProcessors()
    {
        return _preProcessors;
    }

    public IList<Type> GetPostProcessors()
    {
        return _postProcessors;
    }

    public IRequestPipeline<TRequest> AddPreProcessor<TRequestPipelineHandler>()
        where TRequestPipelineHandler : IRequestPipelineHandler<TRequest>
    {
        _preProcessors.Add(typeof(TRequestPipelineHandler));

        return this;
    }

    public IRequestPipeline<TRequest> AddPostProcessor<TRequestPipelineHandler>()
        where TRequestPipelineHandler : IRequestPipelineHandler<TRequest>
    {
        _postProcessors.Add(typeof(TRequestPipelineHandler));

        return this;
    }
}