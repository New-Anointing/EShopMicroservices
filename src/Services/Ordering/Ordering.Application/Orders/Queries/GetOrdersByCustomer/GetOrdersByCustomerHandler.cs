


namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext) 
        : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
    {
        public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
        {
            //get orders from db
            //return result
            var orderFromDb = await dbContext.Orders
                .Include(order => order.OrderItems)
                .AsNoTracking()
                .Where(order => order.CustomerId == CustomerId.Of(query.CustomerId))
                .OrderBy(order => order.OrderName.Value)
                .ToListAsync(cancellationToken);
            var orders = orderFromDb.ToOrderDtoList();
            return new GetOrderByCustomerResult(orders);
        }
    }
}
