
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DelteBasketResult>;
    public record DelteBasketResult(bool IsSuccess);

    public class DeleteBasketValidation : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required")
                .Length(2, 50).WithMessage("User name must be between 2 and 50 characters long");
        }
    }

    public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DelteBasketResult>
    {
        public async Task<DelteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteBasketAsync(command.UserName, cancellationToken);
            return new DelteBasketResult(true);
        }
    }
}
