using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.EndPoints
{
   // public record class DeleteOrderRequest(Guid OrderId);
   public record class DeleteOrderResponse(bool IsDeleted);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapDelete("/orders/{orderId:guid}", async (Guid orderId, ISender sender) =>
            {
                var command = new DeleteOrderCommand(orderId);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteOrderResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete an order by its ID")
            .WithDescription("This endpoint allows you to delete an existing order by providing its unique identifier.");
        }
    }
}
