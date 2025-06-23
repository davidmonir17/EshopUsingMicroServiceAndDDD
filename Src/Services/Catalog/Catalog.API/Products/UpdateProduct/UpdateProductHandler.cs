using FluentValidation;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name,
        string Description,
        string ImageFile,
        decimal Price,
        List<string> Category) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required")
                .Length(2, 150).WithMessage("product Name with range 2 to 150 chracters");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price is must be greater than 0");

        }
    }
    public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id);
            if (product is null)
                throw new ProductNotFoundException(command.Id);

            product.Name = command.Name;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
            product.Category = command.Category;

            session.Update(product);
            await session.SaveChangesAsync();
            return new UpdateProductResult(true);

        }
    }
}

