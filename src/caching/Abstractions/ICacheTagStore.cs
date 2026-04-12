namespace Lingua.BuildingBlocks.Caching.Abstractions;

/// <summary>
/// Interface for a store that manages cache tags and their associated keys. This allows for efficient retrieval of cache keys based on tags, enabling features like cache invalidation by tag.
/// </summary>
public interface ICacheTagStore
{
    /// <summary>
    /// Adds a cache key to a specified tag. This allows for grouping cache entries under a common tag, which can later be used for bulk operations like invalidation.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(string tag, string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all cache keys associated with a specified tag. This is useful for operations that need to retrieve or invalidate all cache entries under a particular tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IList<string>> GetKeysAsync(string tag, CancellationToken cancellationToken = default);
}   
