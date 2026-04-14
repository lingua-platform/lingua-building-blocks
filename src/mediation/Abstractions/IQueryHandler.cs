using MediatR;

namespace Lingua.BuildingBlocks.Mediation.Abstractions;

/// <summary>
/// Interface for handling queries in the MediatR pattern. A query represents a request for data that does not change the state of the system.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IQueryHandler<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>;
