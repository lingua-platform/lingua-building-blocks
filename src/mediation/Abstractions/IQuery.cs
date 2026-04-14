using MediatR;

namespace Lingua.BuildingBlocks.Mediation.Abstractions;

/// <summary>
/// Interface representing a query in the CQRS pattern. A query is a request to retrieve data from the system without modifying its state. It is handled by a query handler that processes the request and returns the requested data as a response.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse>;
