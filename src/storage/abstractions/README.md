# Storage Abstractions

The Storage Abstractions project provides a comprehensive, provider-agnostic set of interfaces and models for building storage solutions. It defines contracts for various cloud storage providers (AWS S3, Azure Blob Storage, etc.) ensuring consistent behavior across different implementations.

## Project Structure

```
abstractions/
├── IStorageService.cs           # Core storage operations contract
├── IStorageProvider.cs          # Provider lifecycle and management
├── IStorageHealthCheck.cs       # Health monitoring contract
├── Models/                      # Data models and transfer objects
│   ├── StorageObjectMetadata.cs
│   ├── StorageObjectInfo.cs
│   └── StorageProviderHealth.cs
├── Options/                     # Configuration options for operations
│   ├── UploadOptions.cs
│   ├── DownloadOptions.cs
│   └── ListOptions.cs
├── Exceptions/                  # Custom exception hierarchy
│   └── StorageException.cs
└── README.md
```

## Overview

This project contains:

- **Core Interfaces** (root level): Definitions for storage service operations, provider implementations, and health checks
- **Models** (`Models/`): Data transfer objects for metadata, file information, and health status
- **Options** (`Options/`): Flexible configuration for upload, download, and listing operations
- **Exceptions** (`Exceptions/`): Custom exceptions for storage-specific error handling

## Key Components

### Interfaces (Root Level)

#### `IStorageService`
Core interface for all storage operations including:
- `UploadAsync()` - Upload files as streams or from file paths
- `DownloadAsync()` - Download files as streams or to file paths
- `DeleteAsync()` - Remove files from storage
- `ExistsAsync()` - Check file existence
- `GetMetadataAsync()` - Retrieve file metadata
- `ListAsync()` - List objects with optional prefix filtering
- `DeleteBatchAsync()` - Delete multiple files efficiently
- `GeneratePresignedUrlAsync()` - Generate temporary access URLs

#### `IStorageProvider`
Provider management interface:
- `InitializeAsync()` - Initialize the storage provider
- `ValidateAsync()` - Validate configuration and connectivity
- `GetHealthAsync()` - Get provider health status

#### `IStorageHealthCheck`
Health monitoring interface:
- `CheckHealthAsync()` - Check current health status
- `CheckProviderHealthAsync()` - Check specific provider health
- `CheckAllProvidersHealthAsync()` - Check all configured providers

### Models (`Models/`)

#### `StorageObjectMetadata`
Comprehensive metadata about stored objects:
```csharp
public class StorageObjectMetadata
{
    public required string Key { get; init; }
    public required long Size { get; init; }
    public required string ContentType { get; init; }
    public required DateTimeOffset LastModified { get; init; }
    public required string ETag { get; init; }
    public Dictionary<string, string> CustomMetadata { get; init; }
    public string? StorageClass { get; init; }
    public string? VersionId { get; init; }
}
```

#### `StorageObjectInfo`
Lightweight object information for listing operations:
```csharp
public class StorageObjectInfo
{
    public required string Key { get; init; }
    public required long Size { get; init; }
    public required DateTimeOffset LastModified { get; init; }
    public required string ETag { get; init; }
    public string? StorageClass { get; init; }
}
```

#### `StorageProviderHealth`
Provider health status information:
```csharp
public class StorageProviderHealth
{
    public required string ProviderName { get; init; }
    public required bool IsHealthy { get; init; }
    public required DateTimeOffset LastChecked { get; init; }
    public string? ErrorMessage { get; init; }
    public Dictionary<string, object> Details { get; init; }
}
```

### Options (`Options/`)

#### `UploadOptions`
Configure upload behavior:
- Custom metadata
- Storage class/tier
- Public/private access
- Cache control headers
- Content disposition
- Encryption settings
- Encryption key ID

#### `DownloadOptions`
Configure download behavior:
- Range requests (partial downloads)
- Conditional headers (ETag, modification date)
- Buffer size customization

#### `ListOptions`
Configure listing behavior:
- Prefix filtering
- Delimiter for hierarchical structure
- Pagination with max keys and continuation tokens
- Recursive listing option
- Client-side filtering

### Exceptions (`Exceptions/`)

- `StorageException` - Base exception for all storage operations
- `StorageObjectNotFoundException` - File not found
- `StorageAccessDeniedException` - Permission denied
- `StorageConfigurationException` - Invalid configuration or credentials

## Usage

### Basic Upload and Download
```csharp
// Upload a file
using var fileStream = File.OpenRead("myfile.pdf");
var fileKey = await storageService.UploadAsync(
    "documents/myfile.pdf",
    fileStream,
    "application/pdf",
    metadata: new() { { "owner", "john@example.com" } });

// Download the file
using var downloadStream = await storageService.DownloadAsync("documents/myfile.pdf");
await using var outputFile = File.Create("downloaded.pdf");
await downloadStream.CopyToAsync(outputFile);
```

### Check File Existence and Get Metadata
```csharp
bool exists = await storageService.ExistsAsync("documents/myfile.pdf");
if (exists)
{
    var metadata = await storageService.GetMetadataAsync("documents/myfile.pdf");
    Console.WriteLine($"Size: {metadata.Size} bytes");
    Console.WriteLine($"Last Modified: {metadata.LastModified}");
}
```

### List Files with Prefix
```csharp
var files = storageService.ListAsync(prefix: "documents/");
await foreach (var file in files)
{
    Console.WriteLine($"{file.Key} - {file.Size} bytes");
}
```

### Generate Pre-signed URL
```csharp
var url = await storageService.GeneratePresignedUrlAsync(
    "documents/myfile.pdf",
    expirationMinutes: 60);
// Share URL with external users for temporary access
```

### Delete Multiple Files
```csharp
var keys = new[] { "old-file1.pdf", "old-file2.pdf", "old-file3.pdf" };
int deletedCount = await storageService.DeleteBatchAsync(keys);
Console.WriteLine($"Deleted {deletedCount} files");
```

### Health Checks
```csharp
var health = await healthCheck.CheckHealthAsync();
if (health.IsHealthy)
{
    Console.WriteLine("Storage service is operational");
}
else
{
    Console.WriteLine($"Storage issue: {health.ErrorMessage}");
}
```

## Implementation Guidelines

When implementing storage providers based on these abstractions:

1. **Stream Management**: Handle stream disposal properly in upload/download operations
2. **Error Mapping**: Map provider-specific errors to the defined `StorageException` hierarchy
3. **Async Operations**: Always use async/await patterns with proper cancellation token support
4. **Metadata Preservation**: Capture and preserve file metadata accurately
5. **Concurrency**: Handle concurrent operations safely
6. **Validation**: Validate input parameters (keys, content types, etc.) before operations
7. **Retry Logic**: Implement appropriate retry mechanisms for transient failures
8. **Logging**: Log significant operations and errors for troubleshooting

## Supported Target Frameworks

- .NET 8.0
- .NET 9.0

## Dependencies

No external dependencies - this is a pure abstraction project.

## Related Implementations

- `Lingua.BuildingBlocks.Storage.AwsS3` - AWS S3 implementation
- `Lingua.BuildingBlocks.Storage.AzureBlobStorage` - Azure Blob Storage implementation

## License

See LICENSE file in the root directory.
