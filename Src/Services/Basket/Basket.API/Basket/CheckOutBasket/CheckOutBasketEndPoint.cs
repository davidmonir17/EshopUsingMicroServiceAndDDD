
using Basket.API.Basket.StoreBasket;
using Basket.API.Dtos;

namespace Basket.API.Basket.CheckOutBasket
{

    public record checkoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);
    public record checkoutBaskitResponse( bool IsSuccess);
    public class CheckOutBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (checkoutBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<checkoutBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<checkoutBaskitResponse>();
                return Results.Ok(response);

            })
                .WithName("checkoutBasket")
                .Produces<checkoutBaskitResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("checkoutBasket a user's shopping basket")
                .WithDescription("This endpoint allows you to checkoutBasket a user's shopping basket by providing the shopping cart details.");

        }
    }
}
