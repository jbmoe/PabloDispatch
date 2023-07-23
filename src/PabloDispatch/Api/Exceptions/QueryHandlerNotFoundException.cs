namespace PabloDispatch.Api.Exceptions;

public class QueryHandlerNotFoundException : Exception
{
    private QueryHandlerNotFoundException(Type queryType, Type resultType)
        : base($"QueryHandler not found for query {queryType} with result {resultType}.")
    {
    }

    public static QueryHandlerNotFoundException FromTypes<TQuery, TResult>()
    {
        return new QueryHandlerNotFoundException(typeof(TQuery), typeof(TResult));
    }
}