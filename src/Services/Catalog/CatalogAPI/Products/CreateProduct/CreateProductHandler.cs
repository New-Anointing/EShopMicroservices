using MediatR;

namespace CatalogAPI.Products.CreateProduct;

//request sent from the http client
public record CreateProductCommand(string Name,List<string> Category, string Description, string ImageFile,decimal Price)
    : IRequest<CreateProductResult>;

//result returned back
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Logic to create new product
        throw new NotImplementedException();
    }
}