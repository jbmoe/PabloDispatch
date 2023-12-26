namespace PabloDispatch.Api.Exceptions;

public class QueryOptionsProviderNotFoundException : Exception
{
    private QueryOptionsProviderNotFoundException(Type queryType, Type resultType)
        : base($"QueryOptionsProvider not found for query {queryType} with result {resultType}.")
    {
    }

    public static QueryOptionsProviderNotFoundException FromTypes<TQuery, TResult>()
    {
        return new QueryOptionsProviderNotFoundException(typeof(TQuery), typeof(TResult));
    }
}