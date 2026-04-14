using MediatR;

namespace Lingua.BuildingBlocks.Mediation.Abstractions;

/// <summary>
/// Interface for handling commands in the MediatR pattern. A command represents an action that changes the state of the system.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface ICommandHandler<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>;
