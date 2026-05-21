# lingua-building-blocks

`lingua-building-blocks` is a repository for shared platform building blocks used across the Lingua ecosystem. Its goal is to provide reusable abstractions, interfaces, and helpers for multiple services and applications.

## Purpose

As Lingua grows, maintaining consistency and avoiding duplicate implementation becomes critical. This repository supports those goals by:

- Standardizing common abstractions for caching, mediation, storage, and more.
- Providing libraries that can be packaged as NuGet packages and reused easily.
- Reducing development time by reusing prebuilt components.
- Improving maintainability by centralizing shared rules and logic.
- Ensuring a consistent approach to cross-cutting concerns.

## Structure

This repo is organized as a monorepo with multiple .NET projects under the `src/` folder.

```
lingua-building-blocks/
├── src/
│   ├── caching/
│   │   ├── Lingua.BuildingBlocks.Caching.csproj
│   │   └── README.md
│   ├── mediation/
│   │   ├── Lingua.BuildingBlocks.Mediation.csproj
│   │   └── README.md
│   └── storage/
│       ├── abstractions/
│       │   ├── Lingua.BuildingBlocks.Storage.Abstractions.csproj
│       │   └── README.md
│       ├── aws-s3/
│       │   └── Lingua.BuildingBlocks.Storage.AwsS3.csproj
│       └── azure-blob-storage/
│           └── Lingua.BuildingBlocks.Storage.AzureBlobStorage.csproj
├── packages.json
└── LICENSE
```

## Current building blocks

### 1. Caching (`src/caching`)

This library provides abstractions and helpers for building effective caching strategies:

- `ICacheService` and related interfaces
- Dynamic cache key construction
- Tag-based invalidation
- Distributed locking for clustered environments
- Flexible cache configuration options

The module aims to reduce redundant data access, improve performance, and enable consistent cache usage across Lingua services.

See: [src/caching/README.md](src/caching/README.md)

### 2. Mediation (`src/mediation`)

This library defines abstractions for CQRS/mediator-style architecture:

- `ICommand`, `IQuery`
- `ICommandHandler`, `IQueryHandler`
- Separates request dispatch from business logic handling

The module helps create cleaner, more testable, and maintainable code, following common patterns for enterprise applications.

See: [src/mediation/README.md](src/mediation/README.md)

### 3. Storage (`src/storage`)

This section includes abstractions and providers for object storage:

- `IStorageService`, `IStorageProvider`, `IStorageHealthCheck`
- `StorageObjectInfo`, `StorageObjectMetadata`, `StorageProviderHealth`
- Providers for AWS S3 and Azure Blob Storage
- Flexible upload/download/list options

The goal is to provide a common storage abstraction layer that makes it easier to switch between storage providers.

See: `src/storage/README.md`

## Usage

Each building block can be packaged and used independently. Add the package to your .NET project by referencing the corresponding NuGet package.

Example:

```bash
dotnet add package Lingua.BuildingBlocks.Caching
```

Or add it to your `.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="Lingua.BuildingBlocks.Mediation" Version="[Latest_Version]" />
</ItemGroup>
```

Replace `[Latest_Version]` with the actual package version available on Lingua's internal feed.

## Packaging and publishing

This repo includes `packages.json` to map package names to project paths for CI/CD pipelines. The process typically uses GitHub Actions from the `lingua-cicd` repository to build, pack, and publish packages.

## Contributing

When modifying or adding a module:

- Update the module-specific README in the relevant folder.
- Keep shared APIs and contracts stable.
- Verify build and unit tests before merging.

## License

`lingua-building-blocks` is licensed under the MIT License. See `LICENSE` for details.
