using PabloCache.Abstractions.Api.Services;
using PabloDispatch.Api.Services;

namespace PabloDispatch.Domain.Services;

/// <summary>
/// Partial base class.
/// </summary>
public partial class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICacheService _cacheService;

    public Dispatcher(IServiceProvider serviceProvider, ICacheService cacheService)
    {
        _serviceProvider = serviceProvider;
        _cacheService = cacheService;
    }
}