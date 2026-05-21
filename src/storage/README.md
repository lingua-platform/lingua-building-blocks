# Storage Building Block

Comprehensive cloud storage abstraction and implementations for the Lingua platform. Provides a unified interface for working with multiple storage providers including AWS S3 and Azure Blob Storage.

## Overview

The Storage building block enables applications to interact with cloud storage services through a consistent, provider-agnostic API. This eliminates vendor lock-in and allows switching between providers with minimal code changes.

## Projects

### 📦 Abstractions (`abstractions/`)

Provider-agnostic interfaces and models defining the storage contract.

**Key Interfaces:**
- `IStorageService` - Core storage operations (upload, download, delete, list, metadata)
- `IStorageProvider` - Provider initialization and validation
- `IStorageHealthCheck` - Health monitoring

**Key Models:**
- `StorageObjectMetadata` - Detailed file metadata
- `StorageObjectInfo` - Lightweight file information
- `StorageProviderHealth` - Health status
- `UploadOptions`, `DownloadOptions`, `ListOptions` - Operation configuration

**Supported Frameworks:** .NET 8.0, .NET 9.0

**See:** [Abstractions README](./abstractions/README.md)

### ☁️ AWS S3 Implementation (`aws-s3/`)

AWS S3 provider implementation for the storage abstractions.

**Features:**
- Full `IStorageService` implementation
- S3-specific features (storage classes, lifecycle policies)
- Efficient batch operations
- Pre-signed URL support
- Server-side encryption

**Supported Frameworks:** .NET 8.0, .NET 9.0

### 🔵 Azure Blob Storage Implementation (`azure-blob-storage/`)

Azure Blob Storage provider implementation for the storage abstractions.

**Features:**
- Full `IStorageService` implementation
- Azure-specific features (tiers, access tiers)
- Blob snapshots and versioning
- Pre-signed SAS URL generation
- Hierarchical namespace support (Data Lake)

**Supported Frameworks:** .NET 8.0, .NET 9.0

## Quick Start

### Installation

Add the desired NuGet packages to your project:

```bash
# For abstractions only
dotnet add package Lingua.BuildingBlocks.Storage.Abstractions

# For AWS S3
dotnet add package Lingua.BuildingBlocks.Storage.AwsS3

# For Azure Blob Storage
dotnet add package Lingua.BuildingBlocks.Storage.AzureBlobStorage
```

### Basic Usage

#### AWS S3
```csharp
// Configuration
services.AddAwsS3Storage(options =>
{
    options.BucketName = "my-bucket";
    options.Region = "us-east-1";
});

// Usage
var storage = serviceProvider.GetRequiredService<IStorageService>();
await storage.UploadAsync("documents/file.pdf", stream, "application/pdf");
var metadata = await storage.GetMetadataAsync("documents/file.pdf");
```

#### Azure Blob Storage
```csharp
// Configuration
services.AddAzureBlobStorage(options =>
{
    options.ConnectionString = "DefaultEndpointsProtocol=https;...";
    options.ContainerName = "my-container";
});

// Usage
var storage = serviceProvider.GetRequiredService<IStorageService>();
await storage.UploadAsync("documents/file.pdf", stream, "application/pdf");
var metadata = await storage.GetMetadataAsync("documents/file.pdf");
```

## Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    Application Layer                         │
│                  (Business Services)                          │
└────────────────────────┬────────────────────────────────────┘
                         │
                         │ IStorageService
                         │
┌────────────────────────┴────────────────────────────────────┐
│           Storage Building Block Abstractions               │
│  (IStorageService, IStorageProvider, IStorageHealthCheck)  │
└──────┬────────────────────────────────────────────────┬─────┘
       │                                                │
       │                                                │
┌──────▼──────────┐                          ┌─────────▼──────┐
│   AWS S3        │                          │  Azure Blob    │
│  Implementation │                          │ Implementation │
└─────────────────┘                          └────────────────┘
       │                                                │
       │ AWSSDK.S3                                      │
       │                                    Azure.Storage.Blobs
       │                                                │
       ▼                                                ▼
┌─────────────────────────────────────────────────────────────┐
│                  Cloud Provider APIs                         │
│            (AWS S3 / Azure Blob Storage)                     │
└─────────────────────────────────────────────────────────────┘
```

## Common Scenarios

### Upload File with Custom Metadata
```csharp
var options = new UploadOptions
{
    Metadata = new() 
    { 
        { "owner", "user@example.com" },
        { "department", "finance" }
    },
    CacheControl = "max-age=3600",
    IsPublic = false
};

await storage.UploadFileAsync(
    "reports/2024-q1.pdf",
    "/local/path/report.pdf",
    "application/pdf",
    options.Metadata,
    cancellationToken: cancellationToken);
```

### Paginated Listing
```csharp
var options = new ListOptions
{
    Prefix = "documents/2024/",
    MaxKeys = 100
};

var files = storage.ListAsync("documents/2024/");
await foreach (var file in files)
{
    Console.WriteLine($"{file.Key}: {file.Size} bytes (Modified: {file.LastModified})");
}
```

### Conditional Download
```csharp
var options = new DownloadOptions
{
    IfNoneMatch = previousETag,  // Skip if file hasn't changed
    Range = "bytes=0-1023"        // Download first 1KB only
};

try
{
    var stream = await storage.DownloadAsync("documents/file.pdf");
    // Process stream
}
catch (StorageObjectNotFoundException ex)
{
    Console.WriteLine($"File {ex.Key} not found");
}
```

### Health Monitoring
```csharp
var healthCheck = serviceProvider.GetRequiredService<IStorageHealthCheck>();
var health = await healthCheck.CheckAllProvidersHealthAsync();

foreach (var provider in health)
{
    Console.WriteLine($"{provider.ProviderName}: {(provider.IsHealthy ? "✓ OK" : "✗ FAILED")}");
    if (!provider.IsHealthy)
    {
        Console.WriteLine($"  Error: {provider.ErrorMessage}");
    }
}
```

## Error Handling

The storage services throw exceptions from the `StorageException` hierarchy:

```csharp
try
{
    await storage.DownloadAsync("documents/nonexistent.pdf");
}
catch (StorageObjectNotFoundException ex)
{
    Console.WriteLine($"File not found: {ex.Key}");
}
catch (StorageAccessDeniedException ex)
{
    Console.WriteLine($"Access denied: {ex.Message}");
}
catch (StorageConfigurationException ex)
{
    Console.WriteLine($"Configuration error: {ex.Message}");
}
catch (StorageException ex)
{
    Console.WriteLine($"Storage error: {ex.Message}");
}
```

## Best Practices

1. **Use Dependency Injection** - Always register storage services in DI container
2. **Handle Cancellation** - Always pass `CancellationToken` for responsive applications
3. **Dispose Streams** - Use `using` statements for stream management
4. **Batch Operations** - Use `DeleteBatchAsync()` for multiple deletions
5. **Health Checks** - Implement health checks for infrastructure monitoring
6. **Error Handling** - Catch specific exception types for proper error recovery
7. **Pre-signed URLs** - Use for temporary, controlled access to files
8. **Metadata** - Leverage custom metadata for file organization and tracking

## Configuration

### Environment Variables

Common configuration approaches:

**AWS S3:**
```
AWS_ACCESS_KEY_ID=your-key
AWS_SECRET_ACCESS_KEY=your-secret
AWS_REGION=us-east-1
STORAGE_BUCKET_NAME=my-bucket
```

**Azure Blob Storage:**
```
AZURE_STORAGE_CONNECTION_STRING=your-connection-string
AZURE_STORAGE_CONTAINER_NAME=my-container
```

### appsettings.json

```json
{
  "Storage": {
    "Provider": "AWS_S3",
    "S3": {
      "BucketName": "my-bucket",
      "Region": "us-east-1",
      "EncryptionKeyId": "arn:aws:kms:..."
    },
    "AzureBlob": {
      "ConnectionString": "DefaultEndpointsProtocol=https;...",
      "ContainerName": "my-container"
    }
  }
}
```

## Testing

Each implementation includes:
- Unit tests for core functionality
- Integration tests with actual storage providers
- Mock implementations for testing dependent services

## Performance Considerations

- **Batch Operations**: Use `DeleteBatchAsync()` for multiple file deletions
- **Streaming**: For large files, use stream-based upload/download to reduce memory usage
- **Pagination**: Use `ListAsync()` with appropriate `MaxKeys` to avoid loading large result sets
- **Pre-signed URLs**: Use for long-term access instead of repeated authentication
- **Buffering**: Customize buffer sizes for different network conditions

## Security

- **Encryption**: By default, upload operations use server-side encryption
- **Access Control**: Use provider-specific IAM policies to restrict access
- **Pre-signed URLs**: Generate with limited expiration times
- **Custom Metadata**: Avoid storing sensitive information
- **Credentials**: Use environment variables or secure vaults, never hardcode

## Troubleshooting

### Connection Issues
- Verify credentials and permissions
- Check network connectivity
- Review security group/firewall rules
- Run `CheckHealthAsync()` to diagnose

### Upload Failures
- Verify bucket/container exists and is accessible
- Check object key format validity
- Ensure sufficient permissions
- Review network connectivity for large uploads

### Performance Issues
- Use batch operations for multiple deletions
- Implement pagination for large listings
- Consider upload/download buffer sizes
- Profile with CloudWatch (AWS) or Application Insights (Azure)

## Related Building Blocks

- **Caching** - Combine with caching for frequently accessed files
- **Mediation** - Use with CQRS pattern for structured commands
- **Domain** - Integrate with domain entities for file associations

## License

See LICENSE file in the parent directory.

## Contributing

See CONTRIBUTING.md for guidelines on extending storage implementations.
