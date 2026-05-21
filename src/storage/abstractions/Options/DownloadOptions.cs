namespace Lingua.BuildingBlocks.Storage.Abstractions.Options;

/// <summary>
/// Configuration options for file download operations.
/// </summary>
public class DownloadOptions
{
    /// <summary>
    /// Optional range header for partial downloads (e.g., "bytes=0-1023" for first 1024 bytes).
    /// </summary>
    public string? Range { get; set; }

    /// <summary>
    /// Optional ETag value for conditional downloads (download only if ETag doesn't match).
    /// Useful for cache validation and avoiding redundant downloads.
    /// </summary>
    public string? IfNoneMatch { get; set; }

    /// <summary>
    /// Optional ETag value for conditional downloads (download only if ETag matches).
    /// Useful for ensuring you get a specific version of a file.
    /// </summary>
    public string? IfMatch { get; set; }

    /// <summary>
    /// Optional value indicating the download should only proceed if the file hasn't been modified since this date.
    /// </summary>
    public DateTimeOffset? IfModifiedSince { get; set; }

    /// <summary>
    /// Optional value indicating the download should only proceed if the file has been modified since this date.
    /// </summary>
    public DateTimeOffset? IfUnmodifiedSince { get; set; }

    /// <summary>
    /// Optional buffer size in bytes for streaming downloads (defaults to provider-specific value).
    /// </summary>
    public int? BufferSize { get; set; }
}
