namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            //get orders from db
            //return result
            var ordersFromDb = await dbContext.Orders
                .Include(order => order.OrderItems)
                .AsNoTracking()
                .Where(order => order.OrderName.Value.Contains(query.Name))
                .OrderBy(order => order.OrderName)
                .ToListAsync(cancellationToken);

            var orders = ordersFromDb.ToOrderDtoList();

            return new GetOrdersByNameResult(orders);
        }
    }
}
