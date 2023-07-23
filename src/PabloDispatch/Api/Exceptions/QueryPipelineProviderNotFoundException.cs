namespace PabloDispatch.Api.Exceptions;

public class QueryPipelineProviderNotFoundException : Exception
{
    private QueryPipelineProviderNotFoundException(Type queryType, Type resultType)
        : base($"QueryPipelineProvider not found for query {queryType} with result {resultType}.")
    {
    }

    public static QueryPipelineProviderNotFoundException FromTypes<TQuery, TResult>()
    {
        return new QueryPipelineProviderNotFoundException(typeof(TQuery), typeof(TResult));
    }
}