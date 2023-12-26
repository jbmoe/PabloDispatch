using PabloCache.Abstractions.Api.Services;

namespace PabloDispatch.Domain.PabloCache.Services;

/// <summary>
/// Placeholder implementation of <see cref="ICacheService"/> if no caching is needed.
/// </summary>
public class NullCacheService : ICacheService
{
    public Task<T> GetCachedValue<T>(string cacheKey, Func<Task<T>> resolver, int ttlMinutes = 5)
        where T : class
    {
        throw new NotImplementedException("Use ConfigurePabloCache method to configure the cache component during startup.");
    }

    public Task<T?> TryGetCachedValue<T>(string cacheKey, Func<Task<T?>> resolver, int ttlMinutes = 5)
        where T : class
    {
        throw new NotImplementedException("Use ConfigurePabloCache method to configure the cache component during startup.");
    }
}