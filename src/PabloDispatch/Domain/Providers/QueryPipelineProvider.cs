using PabloDispatch.Api.Providers;
using PabloDispatch.Api.Queries;

namespace PabloDispatch.Domain.Providers;

public class QueryPipelineProvider<TQuery, TResult> : IQueryPipelineProvider<TQuery, TResult>
    where TQuery : IQuery
{
    public QueryPipelineProvider(IReadOnlyList<Type> preProcessors, IReadOnlyList<Type> postProcessors)
    {
        PreProcessors = preProcessors;
        PostProcessors = postProcessors;
    }

    public IReadOnlyList<Type> PreProcessors { get; }
    public IReadOnlyList<Type> PostProcessors { get; }
}