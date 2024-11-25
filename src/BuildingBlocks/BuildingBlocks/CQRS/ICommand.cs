using MediatR;

namespace BuildingBlocks.CQRS;

//ICommand interface overload that returns no response
public interface ICommand : ICommand<Unit>{}

//ICommand interface overload that has a generic return type of TResponse
public interface ICommand<out TResult> : IRequest<TResult>
{
    
}