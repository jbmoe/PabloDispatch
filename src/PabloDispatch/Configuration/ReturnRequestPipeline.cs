using PabloDispatch.Api.Requests;

namespace PabloDispatch.Configuration;

internal class ReturnRequestPipeline<TRequest, TResult> : IRequestPipeline<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    private readonly IList<Type> _preProcessors = new List<Type>();
    private readonly IList<Type> _postProcessors = new List<Type>();

    internal IList<Type> GetPreProcessors()
    {
        return _preProcessors;
    }

    internal IList<Type> GetPostProcessors()
    {
        return _postProcessors;
    }

    public IRequestPipeline<TRequest, TResult> AddPreProcessor<TRequestPipelineHandler>()
        where TRequestPipelineHandler : IRequestPipelineHandler<TRequest, TResult>
    {
        _preProcessors.Add(typeof(TRequestPipelineHandler));

        return this;
    }

    public IRequestPipeline<TRequest, TResult> AddPostProcessor<TRequestPipelineHandler>()
        where TRequestPipelineHandler : IRequestPipelineHandler<TRequest, TResult>
    {
        _postProcessors.Add(typeof(TRequestPipelineHandler));

        return this;
    }
}