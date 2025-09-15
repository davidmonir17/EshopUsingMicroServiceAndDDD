using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Queries.GetOrderByCustomer;
using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.EndPoints
{
    //public record GetOrderByNameRequest(string UserName);
    public record GetOrderByNameResponse(IEnumerable<OrderDTO> Orders);
    public class GetOrderByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{userName}", async (string userName, ISender sender) =>
            {
                var query = new GetOrderByNameQuery(userName);
                var result = await sender.Send(query);
                var response =  result.Adapt<GetOrderByNameResponse>();
                return Results.Ok(response);
            }).WithName("GetOrderByName")
            .Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get orders by order name")
            .WithDescription("This endpoint allows you to get orders by providing the order name.");

        }
    }
}
