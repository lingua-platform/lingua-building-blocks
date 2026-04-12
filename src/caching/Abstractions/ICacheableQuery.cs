using Lingua.BuildingBlocks.Caching.Options;

namespace Lingua.BuildingBlocks.Caching.Abstractions;

/// <summary>
/// ICacheableQuery is an interface that serves as a marker for queries that can be cached. By implementing this interface, developers can indicate that the results of a specific query can be stored in a cache for improved performance and efficiency. This allows the caching mechanism to identify which queries are eligible for caching and manage the cache accordingly, ensuring that frequently accessed data can be quickly retrieved without the need for repeated computations or data retrieval operations. By using ICacheableQuery, developers can optimize their applications by leveraging caching strategies for specific queries, ultimately enhancing the overall user experience and reducing latency in data access.
/// </summary>
public interface ICacheableQuery
{
    /// <summary>
    /// CacheKey is a unique identifier for the cache entry associated with the query. It is used to store and retrieve the cached results of the query efficiently. The CacheKey should be designed to reflect the parameters and context of the query, ensuring that different queries with varying parameters generate distinct cache keys. This allows the caching mechanism to accurately manage and serve cached data based on the specific queries being executed, ultimately improving performance by reducing redundant data retrieval and computation.
    /// </summary>
    string CacheKey { get; }

    /// <summary>
    /// CacheOptions is an optional property that provides additional configuration settings for caching the query results. It may include parameters such as expiration time, cache priority, or any other relevant options that influence how the caching mechanism handles the storage and retrieval of the cached data. By specifying CacheOptions, developers can fine-tune the caching behavior for specific queries, allowing for more efficient cache management and ensuring that cached data remains relevant and up-to-date based on the application's requirements.
    /// </summary>
    CacheOptions? Options { get; }
}

