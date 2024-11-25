using MediatR;

namespace BuildingBlocks.CQRS;

//IQuery interface that returns a response of TResponse after each query and the response cannot be null
public interface IQuery<out TResult> : IRequest<TResult>
     where TResult : notnull
{
    
}