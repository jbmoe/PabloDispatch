using PabloDispatch.Api.Options;
using PabloDispatch.Api.Providers;

namespace PabloDispatch.Domain.Providers;

internal class QueryOptionsProvider<TQuery, TResult> : IQueryOptionsProvider<TQuery, TResult>
{
    public QueryOptionsProvider(CacheOptions<TQuery> cacheOptions)
    {
        CacheOptions = cacheOptions;
    }

    public CacheOptions<TQuery> CacheOptions { get; }
}