using PabloDispatch.Api.Options;

namespace PabloDispatch.Api.Providers;

internal interface IQueryOptionsProvider<TQuery>
{
    CacheOptions<TQuery> CacheOptions { get; }
}