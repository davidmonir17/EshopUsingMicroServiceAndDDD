

using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        string Description,
        string ImageFile,
        decimal Price,
        List<string> Category
    //) : IRequest<CreateProductResponse>;
    ) : ICommand<CreateProductResult>;
    public record CreateProductResult(
        Guid Id
    );
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price is must be greater than 0");
        }
    }
    //internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Models.Product
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
}
