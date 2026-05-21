using Lingua.BuildingBlocks.Storage.Abstractions.Models;

namespace Lingua.BuildingBlocks.Storage.Abstractions;

/// <summary>
/// IStorageHealthCheck defines a contract for performing health checks on storage services.
/// Typically used for monitoring and readiness probes in containerized environments.
/// </summary>
public interface IStorageHealthCheck
{
    /// <summary>
    /// Performs a health check on the storage service.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>StorageProviderHealth containing health status information</returns>
    Task<StorageProviderHealth> CheckHealthAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a specific storage provider is healthy.
    /// </summary>
    /// <param name="providerName">The name of the provider to check</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>StorageProviderHealth containing health status information</returns>
    Task<StorageProviderHealth> CheckProviderHealthAsync(
        string providerName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks the health of all configured storage providers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>IEnumerable of StorageProviderHealth for all providers</returns>
    Task<IEnumerable<StorageProviderHealth>> CheckAllProvidersHealthAsync(CancellationToken cancellationToken = default);
}
