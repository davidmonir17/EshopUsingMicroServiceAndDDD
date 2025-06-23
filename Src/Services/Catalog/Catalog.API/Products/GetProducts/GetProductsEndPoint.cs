
namespace Catalog.API.Products.GetProducts
{
    public record GetProductsrequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsrequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();

                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            }).WithName("GetProducts")
              .Produces<GetProductsResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Get all products")
              .WithDescription("This endpoint retrieves all products from the catalog.");
        }
    }
}
