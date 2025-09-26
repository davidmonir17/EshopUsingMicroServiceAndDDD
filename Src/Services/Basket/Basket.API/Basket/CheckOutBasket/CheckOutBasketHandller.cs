using Basket.API.Dtos;
using BuildingBlocks.messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckOutBasket
{
    public record checkoutBasketCommand(BasketCheckoutDto BasketCheckoutDto):ICommand<checkoutBaskitResult>;
    public record checkoutBaskitResult(bool IsSuccess);

    public class checkoutCommandValidator : AbstractValidator<checkoutBasketCommand>
    {
        public checkoutCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("usename can't be null");

        }
    }
    public class CheckOutBasketHandller(IBasketRepository repository , IPublishEndpoint publishEndpoint) : ICommandHandler<checkoutBasketCommand, checkoutBaskitResult>
    {
        public async Task<checkoutBaskitResult> Handle(checkoutBasketCommand Command, CancellationToken cancellationToken)
        {
            var basket= await repository.GetBasketAsync(Command.BasketCheckoutDto.UserName,cancellationToken);
            if (basket == null) {
            return new checkoutBaskitResult(false);

            }
            var eventMessage = Command.BasketCheckoutDto.Adapt<BasketCheckOutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;
            await publishEndpoint.Publish(eventMessage,cancellationToken);
            await repository.DeleteBasketAsync(Command.BasketCheckoutDto.UserName, cancellationToken);
            return new checkoutBaskitResult(true);

        }
    }
}
