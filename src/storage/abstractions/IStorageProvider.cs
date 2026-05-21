using Lingua.BuildingBlocks.Storage.Abstractions.Models;

namespace Lingua.BuildingBlocks.Storage.Abstractions;

/// <summary>
/// IStorageProvider defines the contract for different storage provider implementations.
/// This interface is typically implemented by provider-specific implementations (AWS S3, Azure Blob Storage, etc.).
/// </summary>
public interface IStorageProvider
{
    /// <summary>
    /// Gets the provider name (e.g., "AWS_S3", "AZURE_BLOB_STORAGE").
    /// </summary>
    string ProviderName { get; }

    /// <summary>
    /// Gets the storage service instance for this provider.
    /// </summary>
    IStorageService Service { get; }

    /// <summary>
    /// Initializes the storage provider with the given configuration.
    /// This should be called once during application startup.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    Task InitializeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates the provider's configuration and connectivity.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the configuration is valid and the provider is accessible</returns>
    Task<bool> ValidateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets health information about the provider.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>StorageProviderHealth information</returns>
    Task<StorageProviderHealth> GetHealthAsync(CancellationToken cancellationToken = default);
}
