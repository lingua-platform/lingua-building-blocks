namespace Lingua.BuildingBlocks.Caching.Internal.Helpers;

/// <summary>
/// Constructs cache keys based on a specified format, incorporating versioning and tenant information.
/// </summary>
public static class CacheKeyBuilder
{
    /// <summary>
    /// Builds a cache key using the provided key, tenant ID, and version. The format of the cache key is:
    /// </summary>
    /// <param name="key"></param>
    /// <param name="tenantId"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public static string Build(string key, string? tenantId = null, string? version = "1.0.0")
        => $"{version}:{tenantId ?? "default"}:{key}";

    /// <summary>
    /// Tags a cache key with a specific tag, allowing for grouping and easier invalidation of related cache entries. The format of the tagged cache key is:
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static string Tag(string tag) => $"tag:{tag}";
}
