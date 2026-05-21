namespace Lingua.BuildingBlocks.Storage.Abstractions.Models;

/// <summary>
/// Represents health status information about a storage provider.
/// </summary>
public class StorageProviderHealth
{
    /// <summary>
    /// The name of the storage provider.
    /// </summary>
    public required string ProviderName { get; init; }

    /// <summary>
    /// Whether the provider is currently healthy and accessible.
    /// </summary>
    public required bool IsHealthy { get; init; }

    /// <summary>
    /// The last time the provider was checked.
    /// </summary>
    public required DateTimeOffset LastChecked { get; init; }

    /// <summary>
    /// Optional error message if the provider is unhealthy.
    /// </summary>
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// Optional details about the provider's current status.
    /// </summary>
    public Dictionary<string, object> Details { get; init; } = [];
}
