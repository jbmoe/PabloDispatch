using PabloDispatch.Api.Options;

namespace PabloDispatch.Api.Providers;

internal interface IQueryOptionsProvider<TQuery, TResult>
{
    CacheOptions<TQuery> CacheOptions { get; }
}