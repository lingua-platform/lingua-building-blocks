namespace Lingua.BuildingBlocks.Storage.Abstractions.Models;

/// <summary>
/// Represents brief information about a stored object returned during listing operations.
/// </summary>
public class StorageObjectInfo
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
    /// The date and time when the object was last modified.
    /// </summary>
    public required DateTimeOffset LastModified { get; init; }

    /// <summary>
    /// The entity tag (ETag) of the object.
    /// </summary>
    public required string ETag { get; init; }

    /// <summary>
    /// Optional storage class information.
    /// </summary>
    public string? StorageClass { get; init; }
}
