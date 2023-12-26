namespace PabloDispatch.Api.Options;

/// <summary>
/// Represents a set of cached pipelines for which caching can be selectively enabled or disabled.
/// </summary>
[Flags]
public enum CachedPipelines : byte
{
    /// <summary>
    /// No pipelines has enabled caching.
    /// </summary>
    None = 0,

    /// <summary>
    /// Caching is enabled for pre-processors.
    /// </summary>
    PreProcessors = 1 << 0,

    /// <summary>
    /// Caching is enabled for post-processors.
    /// </summary>
    PostProcessors = 1 << 1,

    /// <summary>
    /// Caching is enabled for all pipelines.
    /// </summary>
    All = PostProcessors | PreProcessors,
}