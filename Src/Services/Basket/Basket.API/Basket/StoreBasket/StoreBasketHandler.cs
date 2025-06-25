


namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string Username);
    public class StoreBasketValidation : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidation()
        {
            RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Shopping cart cannot be null");
            RuleFor(x => x.ShoppingCart.UserName)
            .NotEmpty().WithMessage("User name is required")
            .Length(2, 50).WithMessage("User name must be between 2 and 50 characters long");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.ShoppingCart;
            // Here you would typically store the shopping cart in a database or cache.
            await repository.StoreBasketAsync(cart, cancellationToken);
            return new StoreBasketResult(cart.UserName);
        }
    }
}
