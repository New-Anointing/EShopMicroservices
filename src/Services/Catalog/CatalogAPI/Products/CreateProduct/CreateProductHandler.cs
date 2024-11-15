namespace CatalogAPI.Products.CreateProduct;

//request sent from the http client
public record CreateProductCommand(string Name,List<string> Category, string Description, string ImageFile,decimal Price)
    : ICommand<CreateProductResult>;

//response returned back
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Logic to create new product
        var product = new Product()
        {
            Name = command.Name,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
            Category = command.Category
        };
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }
}