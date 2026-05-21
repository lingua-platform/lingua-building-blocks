namespace Lingua.BuildingBlocks.Storage.Abstractions.Models;

/// <summary>
/// Represents metadata information about a stored object/file.
/// </summary>
public class StorageObjectMetadata
{
    /// <summary>
    /// The unique identifier/path of the object in storage.
    /// </summary>
    public required string Key { get; init; }

    /// <summary>
    /// The size of the object in bytes.
    /// </summary>
    public required long Size { get; init; }

    /// <summary>
    /// The MIME type of the object (e.g., "application/pdf").
    /// </summary>
    public required string ContentType { get; init; }

    /// <summary>
    /// The date and time when the object was last modified.
    /// </summary>
    public required DateTimeOffset LastModified { get; init; }

    /// <summary>
    /// The entity tag (ETag) of the object, useful for cache validation and concurrency checks.
    /// </summary>
    public required string ETag { get; init; }

    /// <summary>
    /// Custom metadata associated with the object (provider-specific or user-defined).
    /// </summary>
    public Dictionary<string, string> CustomMetadata { get; init; } = [];

    /// <summary>
    /// Optional storage class or tier information (e.g., "STANDARD", "GLACIER" for S3).
    /// </summary>
    public string? StorageClass { get; init; }

    /// <summary>
    /// Optional version ID of the object (if versioning is enabled in the storage provider).
    /// </summary>
    public string? VersionId { get; init; }
}
