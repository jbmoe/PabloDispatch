namespace PabloDispatch.Api.Options;

/// <summary>
/// Represents options for caching in the query handling process.
/// </summary>
/// <typeparam name="TQuery">The type of the query for which caching options are configured.</typeparam>
public class CacheOptions<TQuery>
{
    /// <summary>
    /// Gets or sets a function that generates cache keys based on the specified query.
    /// </summary>
    /// <value>
    /// The cache key factory function. Default is a function that returns an empty string.
    /// </value>
    public Func<TQuery, string> CacheKeyFactory { get; set; } = _ => string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether caching is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if caching is enabled; otherwise, <c>false</c>. Default is <c>false</c>.
    /// </value>
    public bool EnableCache { get; set; } = false;

    /// <summary>
    /// Gets or sets the time-to-live (TTL) duration for cached items in minutes.
    /// </summary>
    /// <value>
    /// The TTL duration in minutes. Default is 5.
    /// </value>
    public int TtlMinutes { get; set; } = 5;

    /// <summary>
    /// Gets or sets the pipelines for which caching is enabled.
    /// This property is considered only when <see cref="EnableCache"/> is set to <c>true</c>.
    /// </summary>
    /// <value>
    /// The cached pipelines configuration. Default is <see cref="CachedPipelines.All"/>.
    /// </value>
    public CachedPipelines CachedPipelines { get; set; } = CachedPipelines.All;
}