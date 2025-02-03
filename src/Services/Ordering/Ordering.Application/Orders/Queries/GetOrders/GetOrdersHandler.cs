
namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            //get orders from db
            //retrun result
            var pageIndex = query.paginationRequest.PageIndex;
            var pageSize = query.paginationRequest.PageSize;
            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
            var ordersFromDb = await dbContext.Orders
                .Include(order => order.OrderItems)
                .OrderBy(order => order.OrderName.Value)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(
                new PaginatedResult<OrderDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    ordersFromDb.ToOrderDtoList()));
        }
    }
}
