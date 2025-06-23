
namespace Catalog.API.Products.DeleteProduct
{ //public record  DeleteProductReq(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductCommand(id);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            }).WithName("DeleteProduct")
              .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Delete a product")
              .WithDescription("This endpoint allows you to delete a product from the catalog by its Id.");
        }
    }
}
