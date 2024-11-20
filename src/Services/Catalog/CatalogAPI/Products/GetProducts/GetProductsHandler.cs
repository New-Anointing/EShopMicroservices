namespace CatalogAPI.Products.GetProducts;

//request from http client
public record GetProductsQuery() 
    : IQuery<GetProductsResult>;

// response returned 
public record GetProductsResult(IEnumerable<Product> Products);  

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProducts query");
        logger.LogInformation("GetProductQueryHandler.Handle called with {@Query}", query);
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}