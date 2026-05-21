using Lingua.BuildingBlocks.Storage.Abstractions.Models;

namespace Lingua.BuildingBlocks.Storage.Abstractions.Options;

/// <summary>
/// Configuration options for listing objects in storage.
/// </summary>
public class ListOptions
{
    /// <summary>
    /// Optional prefix to filter objects by (e.g., "documents/" to list only files in documents folder).
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// Optional delimiter for hierarchical listing (typically "/" for folder-like structure).
    /// </summary>
    public string? Delimiter { get; set; }

    /// <summary>
    /// Maximum number of objects to return in a single page (pagination).
    /// </summary>
    public int? MaxKeys { get; set; }

    /// <summary>
    /// Optional continuation token for paginated results.
    /// Returned from previous list operation to get the next page of results.
    /// </summary>
    public string? ContinuationToken { get; set; }

    /// <summary>
    /// Whether to recursively list objects including subdirectories (when Delimiter is used).
    /// Default is false (only list at the specified level).
    /// </summary>
    public bool Recursive { get; set; } = false;

    /// <summary>
    /// Optional filter predicate to apply client-side filtering on results.
    /// </summary>
    public Func<StorageObjectInfo, bool>? Filter { get; set; }
}
