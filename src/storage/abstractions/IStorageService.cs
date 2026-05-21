using Lingua.BuildingBlocks.Storage.Abstractions.Models;

namespace Lingua.BuildingBlocks.Storage.Abstractions;

/// <summary>
/// IStorageService defines the contract for a generic file/blob storage service.
/// Supports operations like upload, download, delete, and metadata retrieval across different storage providers (AWS S3, Azure Blob Storage, etc.).
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Uploads a file to storage.
    /// </summary>
    /// <param name="key">The unique identifier/path for the file in storage</param>
    /// <param name="stream">The file content stream</param>
    /// <param name="contentType">The MIME type of the file (e.g., "application/pdf")</param>
    /// <param name="metadata">Optional metadata to associate with the file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The URI or key of the uploaded file</returns>
    Task<string> UploadAsync(
        string key,
        Stream stream,
        string contentType,
        Dictionary<string, string>? metadata = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a file from a local file path to storage.
    /// </summary>
    /// <param name="key">The unique identifier/path for the file in storage</param>
    /// <param name="filePath">The local file path to upload</param>
    /// <param name="contentType">The MIME type of the file</param>
    /// <param name="metadata">Optional metadata to associate with the file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The URI or key of the uploaded file</returns>
    Task<string> UploadFileAsync(
        string key,
        string filePath,
        string contentType,
        Dictionary<string, string>? metadata = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads a file from storage as a stream.
    /// </summary>
    /// <param name="key">The unique identifier/path of the file in storage</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A stream containing the file content</returns>
    Task<Stream> DownloadAsync(
        string key,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads a file from storage to a local file path.
    /// </summary>
    /// <param name="key">The unique identifier/path of the file in storage</param>
    /// <param name="filePath">The local file path to download to</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DownloadFileAsync(
        string key,
        string filePath,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a file from storage.
    /// </summary>
    /// <param name="key">The unique identifier/path of the file to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(
        string key,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a file exists in storage.
    /// </summary>
    /// <param name="key">The unique identifier/path of the file to check</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the file exists, false otherwise</returns>
    Task<bool> ExistsAsync(
        string key,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets metadata information about a file without downloading the full content.
    /// </summary>
    /// <param name="key">The unique identifier/path of the file</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>StorageObjectMetadata containing file information</returns>
    Task<StorageObjectMetadata> GetMetadataAsync(
        string key,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all objects in a storage container with an optional prefix filter.
    /// </summary>
    /// <param name="prefix">Optional prefix to filter objects by (e.g., folder path)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>IAsyncEnumerable of StorageObjectInfo</returns>
    IAsyncEnumerable<StorageObjectInfo> ListAsync(
        string? prefix = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple files from storage.
    /// </summary>
    /// <param name="keys">Collection of unique identifiers/paths of files to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of successfully deleted files</returns>
    Task<int> DeleteBatchAsync(
        IEnumerable<string> keys,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates a pre-signed URL for temporary access to a file without authentication.
    /// </summary>
    /// <param name="key">The unique identifier/path of the file</param>
    /// <param name="expirationMinutes">How long the URL should be valid (in minutes)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A pre-signed URL for accessing the file</returns>
    Task<string> GeneratePresignedUrlAsync(
        string key,
        int expirationMinutes = 15,
        CancellationToken cancellationToken = default);
}
