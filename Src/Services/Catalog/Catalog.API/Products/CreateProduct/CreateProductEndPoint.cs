
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(
            string Name,
            string Description,
            string ImageFile,
            decimal Price,
            List<string> Category
        //) : IRequest<CreateProductResponse>;
        );
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender mediator) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await mediator.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.Id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create a new product")
                .WithDescription("This endpoint allows you to create a new product in the catalog.");
        }
    }
}
