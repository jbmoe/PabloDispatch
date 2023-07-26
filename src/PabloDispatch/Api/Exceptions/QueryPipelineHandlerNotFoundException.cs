namespace PabloDispatch.Api.Exceptions;

public class QueryPipelineHandlerNotFoundException : Exception
{
    private QueryPipelineHandlerNotFoundException(Type queryType, Type resultType)
        : base($"QueryPipelineHandler not found for query {queryType} with result {resultType}.")
    {
    }

    public static QueryPipelineHandlerNotFoundException FromTypes<TQuery, TResult>()
    {
        return new QueryPipelineHandlerNotFoundException(typeof(TQuery), typeof(TResult));
    }
}