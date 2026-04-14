# lingua-building-blocks

This repository (`lingua-building-blocks`) serves as a central monorepository for foundational abstraction layers and reusable components across the entire Lingua Platform. Its primary goal is to provide a consistent, efficient, and standardized set of building blocks that can be integrated into various services, applications, and serverless functions within the Lingua ecosystem.

## Purpose

As the Lingua Platform grows, maintaining consistency, promoting best practices, and avoiding code duplication across numerous projects becomes critical. This repository addresses these needs by:

*   **Standardizing Abstractions**: Offering well-defined interfaces and base implementations for common concerns such as caching, mediation, messaging, and more.
*   **Enhancing Reusability**: Providing easily consumable NuGet packages that can be integrated into any Lingua project.
*   **Streamlining Development**: Accelerating development cycles by offering readily available, tested, and platform-aligned components.
*   **Improving Maintainability**: Centralizing core logic and patterns, making it easier to manage and update across the platform.
*   **Ensuring Consistency**: Enforcing a unified approach to common architectural patterns and cross-cutting concerns.

## Structure

This repository is organized as a monorepo, containing multiple `.NET` projects (building blocks) under the `src/` directory. Each project represents a distinct abstraction layer and is designed to be published as a separate NuGet package.

```
lingua-building-blocks/
├── .github/                  # GitHub Actions workflows for CI/CD
├── src/                      # Source code for all building blocks
│   ├── caching/              # Caching abstraction layer
│   │   ├── Lingua.BuildingBlocks.Caching.csproj
│   │   └── README.md
│   └── mediation/            # Mediation (CQRS) abstraction layer
│       ├── Lingua.BuildingBlocks.Mediation.csproj
│       └── README.md
├── packages.json             # Maps package names to project paths for CI/CD
└── README.md                 # This file
└── LICENSE
```

## Current Building Blocks

### 1. Caching (`src/caching`)

This building block provides abstractions and utilities for implementing robust caching strategies. It supports both in-memory and distributed caching, offering features like cache key building, tag-based invalidation, and distributed locking to ensure data consistency across multiple service instances. It aims to improve application performance by reducing redundant data retrieval and computation.

For detailed information, usage examples, and specific configurations, please refer to the [Caching README](src/caching/README.md).

### 2. Mediation (`src/mediation`)

This building block implements the Command Query Responsibility Segregation (CQRS) pattern using [MediatR](https://github.com/jbogard/MediatR). It provides a clean way to decouple the sending of commands and queries from their respective handlers, leading to a more modular, testable, and maintainable application architecture. It's a foundational piece for handling in-process messaging and business logic orchestration.

For detailed information, usage examples, and specific configurations, please refer to the [Mediation README](src/mediation/README.md).

## Planned Building Blocks

We continuously evolve our platform by adding new foundational layers. Future building blocks may include:

*   **Messaging**: Abstractions for inter-service communication via message queues or event buses.
*   **Observability**: Components for logging, tracing, and metrics integration.
*   **Resilience**: Implementations for fault tolerance patterns like retries, circuit breakers, and timeouts.

## CI/CD and Package Publishing

This monorepository utilizes a smart CI/CD pipeline, defined in `.github/workflows/publish.yml`, to publish individual NuGet packages. The workflow operates as follows:

1.  **Tag-based Trigger**: Pushing a Git tag in the format `<package-name>-v<version>` (e.g., `caching-v1.0.0`, `mediation-v1.2.3`) triggers the publishing process.
2.  **Project Resolution**: The `publish.yml` workflow reads the `packages.json` file to map the `<package-name>` from the tag to its corresponding `.csproj` file path within the `src/` directory.
3.  **Reusable Workflow**: It then calls a reusable GitHub Actions workflow (`lingua-platform/lingua-cicd/.github/workflows/nuget-publish-monorepo.yml`) from the `lingua-cicd` repository, passing the resolved project path and version.
4.  **NuGet Publish**: The reusable workflow handles the .NET build, pack, and push operations to the configured NuGet feed (e.g., GitHub Packages).

This approach allows for independent versioning and publishing of each building block while managing them in a single repository.

## Installation

To add any of the building blocks to your Lingua Platform project, use the .NET CLI or add a `PackageReference` to your `.csproj` file. For example:

```bash
dotnet add package Lingua.BuildingBlocks.Caching
```

Or in your `.csproj`:

```xml
<ItemGroup>
    <PackageReference Include="Lingua.BuildingBlocks.Mediation" Version="[Latest_Version]" />
</ItemGroup>
```

Remember to replace `[Latest_Version]` with the actual version you intend to use for the specific building block.

## Contribution

This project is maintained by the Lingua Team. For contributions, please follow the internal guidelines and processes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
