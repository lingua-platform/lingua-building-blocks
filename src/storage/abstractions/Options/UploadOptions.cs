namespace Lingua.BuildingBlocks.Storage.Abstractions.Options;

/// <summary>
/// Configuration options for file upload operations.
/// </summary>
public class UploadOptions
{
    /// <summary>
    /// Optional custom metadata to associate with the uploaded file.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = [];

    /// <summary>
    /// Optional storage class or tier for the object (e.g., "STANDARD", "GLACIER" for S3).
    /// </summary>
    public string? StorageClass { get; set; }

    /// <summary>
    /// Whether to make the uploaded file publicly readable (if supported by the provider).
    /// Default is false (private).
    /// </summary>
    public bool IsPublic { get; set; } = false;

    /// <summary>
    /// Optional cache control header value (e.g., "max-age=3600").
    /// </summary>
    public string? CacheControl { get; set; }

    /// <summary>
    /// Optional content disposition header value (e.g., "attachment; filename=myfile.pdf").
    /// </summary>
    public string? ContentDisposition { get; set; }

    /// <summary>
    /// Whether to use server-side encryption (if supported by the provider).
    /// Default is true for security.
    /// </summary>
    public bool UseEncryption { get; set; } = true;

    /// <summary>
    /// Optional encryption key ID (provider-specific, e.g., KMS key ID for AWS).
    /// </summary>
    public string? EncryptionKeyId { get; set; }
}
