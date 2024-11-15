using MediatR;

namespace BuildingBlocks.CQRS;


//ICommandHandler overload that returns no response and takes in a request
public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand
{
    
}

//ICommandHandler overload that takes in a request and returns a response
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> 
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
    
}