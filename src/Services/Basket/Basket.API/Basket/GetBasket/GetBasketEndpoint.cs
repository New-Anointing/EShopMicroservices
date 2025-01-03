namespace Basket.API.Basket.GetBasket
{
    //public record GetBasketRequest(string UserName)
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Basket/{UserName}", async (string UserName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(UserName));
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            })
             .WithName("GetProductById")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .Produces<GetBasketResponse>(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }
    }
}
