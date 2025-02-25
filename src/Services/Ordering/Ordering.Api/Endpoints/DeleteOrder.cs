using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.Api.Endpoints
{
    public record DeleteOrderResponse(bool IsSuccess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{orderId}", async (Guid orderId, ISender sender) =>
            {
                var command = new DeleteOrderCommand(orderId);
                var result = await sender.Send(command);
                var response = new DeleteOrderResponse(result.IsSuccess);
                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Delete Order")
            .WithDescription("Delete an order");
        }
    }
}
