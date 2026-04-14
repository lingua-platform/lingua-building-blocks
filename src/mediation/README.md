# Lingua.BuildingBlocks.Mediation

This repository (`Lingua.BuildingBlocks.Mediation`) is a foundational building block within the Lingua Platform, providing a robust and standardized implementation of the Command Query Responsibility Segregation (CQRS) pattern using [MediatR](https://github.com/jbogard/MediatR). Its primary purpose is to decouple the sending of commands/queries from their handling, promoting a cleaner architecture, improved testability, and better maintainability across all services and applications in the Lingua ecosystem.

## Purpose

In complex applications like the Lingua Platform, managing business logic and data flow can become challenging. This package addresses these challenges by:

*   **Implementing CQRS**: Clearly separating read (queries) and write (commands) operations, leading to a more focused and scalable design.
*   **Decoupling Components**: Reducing direct dependencies between application layers by routing requests through a mediator, allowing components to evolve independently.
*   **Enhancing Testability**: Making it easier to test individual command and query handlers in isolation.
*   **Improving Maintainability**: Centralizing request handling logic, which simplifies understanding and modifying system behavior.
*   **Integrating with MediatR**: Leveraging the popular MediatR library to provide a battle-tested and efficient in-process messaging solution.

## Key Features and Contents

This package provides the essential interfaces and integration points for implementing CQRS with MediatR:

*   **`Abstractions/ICommand.cs`**:
    A marker interface (`ICommand<out TResponse>`) that extends `MediatR.IRequest<TResponse>`. It signifies a request that intends to modify the state of the system and expects a `TResponse` in return.

*   **`Abstractions/ICommandHandler.cs`**:
    An interface (`ICommandHandler<TRequest, TResponse>`) that extends `MediatR.IRequestHandler<TRequest, TResponse>`. Implementations of this interface are responsible for handling specific `ICommand` types and executing the corresponding business logic to change the system's state.

*   **`Abstractions/IQuery.cs`**:
    A marker interface (`IQuery<out TResponse>`) that extends `MediatR.IRequest<TResponse>`. It signifies a request that intends to retrieve data from the system without causing any side effects or modifying its state.

*   **`Abstractions/IQueryHandler.cs`**:
    An interface (`IQueryHandler<TRequest, TResponse>`) that extends `MediatR.IRequestHandler<TRequest, TResponse>`. Implementations of this interface are responsible for handling specific `IQuery` types and returning the requested data.

## Installation

As an internal NuGet package, `Lingua.BuildingBlocks.Mediation` can be added to your Lingua Platform project as a dependency:

```bash
dotnet add package Lingua.BuildingBlocks.Mediation
```

Or by adding it to your `.csproj` file:

```xml
<ItemGroup>
    <PackageReference Include="Lingua.BuildingBlocks.Mediation" Version="[Latest_Version]" />
</ItemGroup>
```
Remember to replace `[Latest_Version]` with the actual version you intend to use.

## Usage

To effectively use this mediation building block, you will typically:

1.  **Define Commands and Queries**: Create concrete classes that implement `ICommand<TResponse>` or `IQuery<TResponse>`.
2.  **Implement Handlers**: Create handler classes that implement `ICommandHandler<TCommand, TResponse>` or `IQueryHandler<TQuery, TResponse>`.
3.  **Register MediatR**: Configure MediatR in your application's dependency injection container.
4.  **Send Requests**: Inject `IMediator` into your services and use it to send commands and queries.

### Example: Defining and Handling a Command

First, define a command to create a user:

```csharp src/mediation/Commands/CreateUserCommand.cs
using Lingua.BuildingBlocks.Mediation.Abstractions;

public record CreateUserCommand(string Username, string Email) : ICommand<Guid>;
```

Next, implement the handler for this command:

```csharp src/mediation/Handlers/CreateUserCommandHandler.cs
using Lingua.BuildingBlocks.Mediation.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Simulate database operation
        Console.WriteLine($"Creating user: {request.Username} ({request.Email})");
        var newUserId = Guid.NewGuid();
        await Task.Delay(50, cancellationToken); // Simulate async work
        return newUserId;
    }
}
```

### Example: Defining and Handling a Query

First, define a query to get user details:

```csharp src/mediation/Queries/GetUserByIdQuery.cs
using Lingua.BuildingBlocks.Mediation.Abstractions;
using System;

public record UserDetailsDto(Guid Id, string Username, string Email);

public record GetUserByIdQuery(Guid UserId) : IQuery<UserDetailsDto>;
```

Next, implement the handler for this query:

```csharp src/mediation/Handlers/GetUserByIdQueryHandler.cs
using Lingua.BuildingBlocks.Mediation.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDetailsDto>
{
    public async Task<UserDetailsDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Simulate fetching user from database
        Console.WriteLine($"Fetching user with ID: {request.UserId}");
        await Task.Delay(50, cancellationToken); // Simulate async work
        return new UserDetailsDto(request.UserId, "Jane Doe", "jane.doe@example.com");
    }
}
```

### Registering MediatR (Startup example)

In your application's `Program.cs` or `Startup.cs`:

```csharp Program.cs
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
    // You might also register handlers from other assemblies in your solution
    // cfg.RegisterServicesFromAssemblies(typeof(CreateUserCommand).Assembly);
});

// ... other services and app configuration ...

var app = builder.Build();

// ... further app configuration ...

app.Run();
```

### Sending Requests

```csharp ApiController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        Guid userId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetUser), new { userId = userId }, userId);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var query = new GetUserByIdQuery(userId);
        UserDetailsDto user = await mediator.Send(query);
        return Ok(user);
    }
}
```

## Contribution

This project is maintained by the Lingua Team. For contributions, please follow the internal guidelines and processes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file in the root of the `lingua-building-blocks` repository for details.