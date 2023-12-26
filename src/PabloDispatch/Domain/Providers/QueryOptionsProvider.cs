using PabloDispatch.Api.Options;
using PabloDispatch.Api.Providers;

namespace PabloDispatch.Domain.Providers;

internal class QueryOptionsProvider<TQuery> : IQueryOptionsProvider<TQuery>
{
    public QueryOptionsProvider(CacheOptions<TQuery> cacheOptions)
    {
        CacheOptions = cacheOptions;
    }

    public CacheOptions<TQuery> CacheOptions { get; }
}