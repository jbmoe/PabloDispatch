using PabloDispatch.Api.Providers;
using PabloDispatch.Api.Requests;

namespace PabloDispatch.Domain.Providers;

public class ReturnRequestPipelineProvider<TRequest, TResult> : IRequestPipelineProvider<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    public ReturnRequestPipelineProvider(IList<Type> preProcessors, IList<Type> postProcessors)
    {
        PreProcessors = preProcessors;
        PostProcessors = postProcessors;
    }

    public IList<Type> PreProcessors { get; }
    public IList<Type> PostProcessors { get; }
}