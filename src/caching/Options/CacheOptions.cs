namespace Lingua.BuildingBlocks.Caching.Options;

/// <summary>
/// CacheOptions is a class that represents the configuration options for caching in an application. It contains properties that define how caching should behave, such as the expiration time for cache entries. This class can be used to configure caching settings in a centralized manner, allowing developers to easily manage and customize caching behavior across the application. By using CacheOptions, developers can ensure that cached data is appropriately managed and refreshed based on the specified expiration time, improving the performance and efficiency of the application.
/// </summary>
public class CacheOptions
{
    /// <summary>
    /// Expiration is a nullable TimeSpan property that represents the duration for which a cache entry should be considered valid. If set, it indicates the time after which the cached data will expire and should be refreshed or removed from the cache. If null, it implies that the cache entry does not have an expiration time and will remain valid indefinitely until explicitly removed or updated.
    /// </summary>
    public TimeSpan? Expiration { get; set; }

    /// <summary>
    /// UseMemoryCache is a boolean property that indicates whether to use an in-memory cache for storing cached data. If set to true, the application will utilize an in-memory caching mechanism, which can provide faster access to cached data but may consume more memory. If set to false, the application may use an alternative caching strategy, such as distributed caching or a custom cache implementation, depending on the specific requirements and configuration of the application.
    /// </summary>
    public bool UseMemoryCache { get; set; } = true;

    /// <summary>
    /// Size is a nullable long property that represents the maximum size of the cache in bytes. If set, it indicates the total amount of memory that can be used for caching. When the cache reaches this size limit, it may evict older or less frequently accessed entries to make room for new ones. If null, it implies that there is no size limit for the cache, and it can grow indefinitely based on the available memory and caching needs of the application.
    /// </summary>
    public long? Size { get; set; }

    /// <summary>
    /// MemoryThreshold is a long property that represents the threshold size in bytes for determining when to use in-memory caching. If the size of the data to be cached exceeds this threshold, the application may choose to use an alternative caching strategy instead of in-memory caching to avoid consuming excessive memory. The default value is set to 50KB (50 * 1024 bytes), which means that if the data size exceeds this threshold, the application may opt for a different caching mechanism that is more suitable for larger data sets.
    /// </summary>
    public long MemoryThreshold { get; set; } = 50 * 1024; // 50KB

    /// <summary>
    /// Tag is a nullable string property that can be used to assign a specific tag or label to cache entries. This tag can be useful for categorizing or grouping cache entries, allowing for easier management and retrieval of cached data based on the assigned tags. For example, developers can use tags to identify cache entries related to specific features, modules, or data types, making it easier to invalidate or refresh related cache entries when necessary. If null, it implies that the cache entry does not have an associated tag and may be treated as a general cache entry without any specific categorization.
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// StaleTime is a nullable DateTime property that represents the point in time when a cache entry is considered stale or outdated. If set, it indicates the specific date and time after which the cached data should be considered stale and may need to be refreshed or updated. This can be useful for scenarios where cached data has a known validity period or when there are specific events that trigger the need for cache invalidation. If null, it implies that there is no specific stale time defined for the cache entry, and it may be considered valid until it expires based on the Expiration property or until it is explicitly removed or updated.
    /// </summary>
    public DateTime? StaleTime { get; set; }

    /// <summary>
    /// UseDistributedLock is a boolean property that indicates whether to use a distributed lock mechanism when accessing or modifying cache entries. If set to true, the application will utilize a distributed locking strategy to ensure that only one instance of the application can access or modify a specific cache entry at a time, which can
    /// </summary>
    public bool UseDistributedLock { get; set; } = true;

    /// <summary>
    /// TenantId is a nullable string property that represents the identifier for a tenant in a multi-tenant application. This property can be used to associate cache entries with specific tenants, allowing for tenant-specific caching strategies and ensuring that cached data is properly isolated and managed based on the tenant context. By using TenantId, developers can implement caching mechanisms that cater to the unique needs of each tenant while maintaining the overall integrity and performance of the caching system. If null, it implies that the cache entry is not associated with any specific tenant and may be treated as a general cache entry applicable to all tenants.
    /// </summary>
    public string? TenantId { get; set; }

    /// <summary>
    /// Version is a string property that represents the version of the cache configuration or implementation. This can be useful for tracking changes to the caching strategy or for compatibility purposes when working with different versions of the caching library or framework. By specifying a version, developers can ensure that they are using the correct configuration and can easily manage updates or changes to the caching behavior as needed. The default value is set to "1.0.0", indicating the initial version of the cache configuration.
    /// </summary>
    public string Version { get; set; } = "1.0.0";
}

