using PabloDispatch.Api.Providers;
using PabloDispatch.Api.Requests;

namespace PabloDispatch.Domain.Providers;

public class VoidRequestPipelineProvider<TRequest> : IRequestPipelineProvider<TRequest>
    where TRequest : IRequest
{
    public VoidRequestPipelineProvider(IList<Type> preProcessors, IList<Type> postProcessors)
    {
        PreProcessors = preProcessors;
        PostProcessors = postProcessors;
    }

    public IList<Type> PreProcessors { get; }
    public IList<Type> PostProcessors { get; }
}