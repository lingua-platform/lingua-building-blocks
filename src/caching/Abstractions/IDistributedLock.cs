namespace Lingua.BuildingBlocks.Caching.Abstractions;

/// <summary>
/// IDistributedLock is an interface that represents a distributed locking mechanism in an application. It provides a contract for implementing distributed locks, which are used to ensure that only one instance of an application can access or modify a specific resource at a time in a distributed environment. By implementing this interface, developers can create custom distributed lock implementations that can be integrated into their applications to manage concurrent access to shared resources
/// </summary>
public interface IDistributedLock
{
    /// <summary>
    /// AcquireAsync is an asynchronous method that attempts to acquire a distributed lock based on the provided key. If the lock is successfully acquired, it returns an IAsyncDisposable instance that can be used to release the lock when it is no longer needed. This method allows developers to manage concurrent access to shared resources in a distributed environment by ensuring that only one instance of the application can hold the lock for a specific key at any given time. By using AcquireAsync, developers can implement synchronization mechanisms
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<IAsyncDisposable> AcquireAsync(string key);
}