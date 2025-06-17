using BuldingBlocks.CQRS;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        string Description,
        string ImageFile,
        decimal Price,
        List<string> Category
    //) : IRequest<CreateProductResponse>;
    ) : ICommand<CreateProductResponse>;
    public record CreateProductResponse(
        Guid Id
    );
    //internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Models.Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Category = command.Category
            };
            return new CreateProductResponse(product.Id);
        }
    }
}
