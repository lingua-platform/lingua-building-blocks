namespace Lingua.BuildingBlocks.Caching.Abstractions;

/// <summary>
/// ICacheMetrics is an interface that represents the contract for a caching metrics system in an application. It provides a way to track and measure various aspects of the caching mechanism, such as cache hits, cache misses, cache evictions, and other relevant metrics that can help developers monitor the performance and effectiveness of their caching strategy. By implementing this interface, developers can create custom caching metrics implementations that can be integrated into their applications to gain insights into the behavior of the cache and make informed decisions about optimizing caching performance and efficiency.
/// </summary>
public interface ICacheMetrics
{
    /// <summary>
    /// Hit is a method that records a cache hit for a specific key. When a cache hit occurs, it indicates that the requested data was successfully retrieved from the cache, which can help developers understand the effectiveness of their caching strategy and identify areas for improvement. By calling the Hit method with the relevant key, developers can track cache hits and analyze the performance of their caching mechanism over time, allowing them to make informed decisions about optimizing cache usage and improving overall application performance.
    /// </summary>
    /// <param name="key"></param>
    void Hit(string key);

    /// <summary>
    /// Miss is a method that records a cache miss for a specific key. When a cache miss occurs, it indicates that the requested data was not found in the cache and had to be retrieved from the original data source, which can help developers understand the limitations of their caching strategy and identify areas for improvement. By calling the Miss method with the relevant key, developers can track cache misses and analyze the performance of their caching mechanism over time, allowing them to make informed decisions about optimizing cache usage and improving overall application performance.
    /// </summary>
    /// <param name="key"></param>
    void Miss(string key);
}

