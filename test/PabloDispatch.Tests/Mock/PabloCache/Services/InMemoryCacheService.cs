using PabloCache.Abstractions.Api.Services;

namespace PabloDispatch.Tests.Mock.PabloCache.Services;

public class InMemoryCacheService : ICacheService
{
    private readonly Dictionary<string, object> _cache = new();

    public async Task<T> GetCachedValue<T>(string cacheKey, Func<Task<T>> resolver, int ttlMinutes = 5)
        where T : class
    {
        if (_cache.TryGetValue(cacheKey, out var cachedValue) && cachedValue is T result)
        {
            return result;
        }

        var resolvedValue = await resolver();
        _cache[cacheKey] = resolvedValue;
        return resolvedValue;
    }

    public async Task<T?> TryGetCachedValue<T>(string cacheKey, Func<Task<T?>> resolver, int ttlMinutes = 5)
        where T : class
    {
        if (_cache.TryGetValue(cacheKey, out var cachedValue) && cachedValue is T result)
        {
            return result;
        }

        var resolvedValue = await resolver();
        if (resolvedValue != null)
        {
            _cache[cacheKey] = resolvedValue;
        }

        return resolvedValue;
    }
}