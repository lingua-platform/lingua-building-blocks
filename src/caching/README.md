# Lingua.BuildingBlocks.Caching

This repository (`Lingua.BuildingBlocks.Caching`) is a core building block within the Lingua Platform, providing a set of abstractions and utilities for implementing caching strategies. Its primary goal is to offer a flexible and standardized way to manage in-memory and distributed caching across various services and components of the Lingua ecosystem, enhancing performance and scalability.

## Purpose

The Lingua Platform comprises numerous services and applications that can benefit from caching to improve response times and reduce the load on backend systems. This package aims to:

*   **Standardize Caching**: Provide a consistent interface for caching operations across the platform.
*   **Support Distributed Caching**: Offer mechanisms for distributed locks and cache tag management to ensure consistency in distributed environments.
*   **Improve Performance**: Enable developers to easily integrate caching to speed up data retrieval and computation.
*   **Promote Best Practices**: Encourage efficient and robust caching strategies through well-defined interfaces and options.

## Key Features and Contents

This package provides the following key components for caching:

*   **`Abstractions/ICacheService.cs`**:
    The central interface for caching operations. It defines methods for retrieving or setting cached values (`GetOrSetAsync`), removing single cache entries (`RemoveAsync`), and invalidating cache entries by tag (`RemoveByTagAsync`).

*   **`Abstractions/ICacheableQuery.cs`**:
    A marker interface that identifies queries whose results can be cached. It allows for query-specific cache keys and `CacheOptions` to be defined directly on the query.

*   **`Options/CacheOptions.cs`**:
    A comprehensive class for configuring individual cache entries. It includes properties for `Expiration` (duration until cache entry is invalid), `UseMemoryCache` (whether to use in-memory caching), `Size` (maximum size for cache entry), `MemoryThreshold` (threshold for in-memory caching), `Tag` (for grouping related cache entries), `StaleTime` (when a cache entry is considered stale), `UseDistributedLock` (to prevent race conditions), `TenantId` (for multi-tenant caching), and `Version` (for cache versioning).

*   **`Abstractions/ICacheMetrics.cs`**:
    An interface for tracking cache performance metrics, such as cache hits and misses, allowing for monitoring and optimization of caching strategies.

*   **`Abstractions/ICacheTagStore.cs`**:
    Defines a contract for a store that manages relationships between cache tags and individual cache keys, enabling efficient invalidation of groups of related cache entries.

*   **`Abstractions/IDistributedLock.cs`**:
    An interface for implementing distributed locking mechanisms, essential for coordinating cache operations across multiple application instances and preventing race conditions during cache updates.

*   **`Abstractions/ICacheWarmService.cs`**:
    A placeholder interface for services responsible for warming up the cache, ensuring frequently accessed data is readily available.

*   **`Internal/Helpers/CacheKeyBuilder.cs`**:
    A utility class for constructing standardized cache keys, incorporating versioning and tenant information for robust cache management.

*   **`Internal/Helpers/SerializationHelper.cs`**:
    Provides generic methods for serializing and deserializing objects, typically used to convert objects to and from a format suitable for storage in cache.

*   **`Internal/Helpers/SizeEstimator.cs`**:
    A simple helper to estimate the size of an object in bytes by serializing it to JSON. Useful for managing cache memory limits.

## Installation

As an internal NuGet package, `Lingua.BuildingBlocks.Caching` can be added to your Lingua Platform project as a dependency:

```bash
dotnet add package Lingua.BuildingBlocks.Caching
```

Or by adding it to your `.csproj` file:

```xml
<ItemGroup>
    <PackageReference Include="Lingua.BuildingBlocks.Caching" Version="[Latest_Version]" />
</ItemGroup>
```
Remember to replace `[Latest_Version]` with the actual version you intend to use.

## Usage

Integrate the caching abstractions into your C# services and applications to leverage standardized caching capabilities.

### Example: Using `ICacheService` with `ICacheableQuery`

First, define a DTO for your data (e.g., `UserDto`):

```csharp
public record UserDto(string Id, string Name);
```

Then, create a query that implements `ICacheableQuery`:

```csharp src/caching/Queries/GetUserDataQuery.cs
using Lingua.BuildingBlocks.Caching.Abstractions;
using Lingua.BuildingBlocks.Caching.Internal.Helpers;
using Lingua.BuildingBlocks.Caching.Options;

public record GetUserDataQuery(string UserId) : ICacheableQuery
{
    public string CacheKey => CacheKeyBuilder.Build($"user:{UserId}", version: "1.0.0");
    public CacheOptions? Options => new CacheOptions
    {
        Expiration = TimeSpan.FromMinutes(5),
        Tag = "users"
    };
}
```

Finally, use `ICacheService` in your application service:

```csharp src/caching/Services/UserService.cs
using Lingua.BuildingBlocks.Caching.Abstractions;
using Lingua.BuildingBlocks.Caching.Internal.Helpers;
using System.Threading;
using System.Threading.Tasks;

public class UserService(ICacheService cacheService)
{
    public async Task<UserDto> GetUserAsync(GetUserDataQuery query, CancellationToken cancellationToken = default)
    {
        return await cacheService.GetOrSetAsync(
            query.CacheKey,
            async () =>
            {
                // Simulate fetching from database
                await Task.Delay(100, cancellationToken);
                return new UserDto(query.UserId, "John Doe");
            },
            query.Options,
            cancellationToken);
    }

    public async Task UpdateUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        // ... logic to update user in database ...

        // Invalidate cache for this user and related tags
        await cacheService.RemoveAsync(CacheKeyBuilder.Build($"user:{userId}"), cancellationToken);
        await cacheService.RemoveByTagAsync("users", cancellationToken);
    }
}
```

## Contribution

This project is maintained by the Lingua Team. For contributions, please follow the internal guidelines and processes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file in the root of the `lingua-building-blocks` repository for details.