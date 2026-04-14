using MediatR;

namespace Lingua.BuildingBlocks.Mediation.Abstractions;

/// <summary>
/// Interface representing a command in the CQRS pattern. A command is a request to perform an action that modifies the state of the system. It is handled by a command handler that executes the requested action and returns a response indicating the result of the operation.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>;