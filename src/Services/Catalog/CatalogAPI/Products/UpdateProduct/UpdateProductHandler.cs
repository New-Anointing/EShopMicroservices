namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, decimal Price, string ImageFile)
    :ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(command => command.Name).NotEmpty()
            .WithMessage("Product Name is required").Length(2, 150).WithMessage("Name should be between 2 and 150 characters");
        RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);
        
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
            throw new ProductNotFoundException();
        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.Category = command.Category;
        product.ImageFile = command.ImageFile;
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);

    }
}