namespace Lingua.BuildingBlocks.Storage.Abstractions.Exceptions;

/// <summary>
/// Base exception for storage-related operations.
/// </summary>
public class StorageException : Exception
{
    public StorageException(string message) : base(message)
    {
    }

    public StorageException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception raised when a requested file/object is not found in storage.
/// </summary>
public class StorageObjectNotFoundException : StorageException
{
    public required string Key { get; init; }

    public StorageObjectNotFoundException(string key, string message) : base(message)
    {
        Key = key;
    }

    public StorageObjectNotFoundException(string key, string message, Exception innerException)
        : base(message, innerException)
    {
        Key = key;
    }
}

/// <summary>
/// Exception raised when a storage operation fails due to access denied or insufficient permissions.
/// </summary>
public class StorageAccessDeniedException : StorageException
{
    public StorageAccessDeniedException(string message) : base(message)
    {
    }

    public StorageAccessDeniedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception raised when a storage operation fails due to invalid configuration or credentials.
/// </summary>
public class StorageConfigurationException : StorageException
{
    public StorageConfigurationException(string message) : base(message)
    {
    }

    public StorageConfigurationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
