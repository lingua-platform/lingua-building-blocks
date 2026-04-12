using Lingua.BuildingBlocks.Caching.Options;

namespace Lingua.BuildingBlocks.Caching.Abstractions;

/// <summary>
/// ICacheService is an interface that defines the contract for a caching service in an application. It provides a method, GetOrSetAsync, which allows developers to retrieve a cached value based on a specified key or set a new value in the cache if it does not already exist. The method takes a key, a factory function to generate the value if it is not present in the cache, optional cache options to configure caching behavior, and a cancellation token for handling cancellation scenarios. By implementing this interface, developers can create custom caching solutions that can be easily integrated into their applications, improving performance and efficiency by reducing the need for repeated data retrieval or computation.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// GetOrSetAsync is an asynchronous method that retrieves a cached value based on the provided key. If the value is not present in the cache, it uses the factory function to generate the value, stores it in the cache with the specified options, and then returns the generated value. This method allows for efficient caching by ensuring that values are only generated when necessary and can be easily retrieved from the cache for subsequent requests, improving performance and reducing redundant computations or data retrieval operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> GetOrSetAsync<T>(
        string key,
        Func<Task<T>> factory,
        CacheOptions? options = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// RemoveAsync is an asynchronous method that removes a cached value from the cache based on the provided key. This method allows developers to explicitly invalidate or clear specific cache entries when they are no longer needed or when the underlying data has changed, ensuring that subsequent requests will trigger the generation of fresh values using the factory function in the GetOrSetAsync method. By providing a way to remove cache entries, this method helps maintain the integrity and relevance of cached data while allowing for efficient cache management in the application.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// RemoveByTagAsync is an asynchronous method that removes cached values from the cache based on a specified tag. This method allows developers to invalidate or clear multiple cache entries that share the same tag, which can be useful for scenarios where related data is cached under a common tag and needs to be invalidated together when the underlying data changes. By providing a way to remove cache entries by tag, this method helps maintain the relevance and consistency of cached data while allowing for efficient cache management in the application, especially when dealing with groups of related cache entries.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveByTagAsync(string tag, CancellationToken cancellationToken = default);
}

