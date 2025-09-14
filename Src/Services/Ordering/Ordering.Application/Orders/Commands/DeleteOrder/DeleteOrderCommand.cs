using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
   public record DeleteOrderCommand(Guid OrderId):ICommand<DeleteOrderResult>;
    public record DeleteOrderResult(bool IsDeleted);

    public class DeleteOrdrerValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrdrerValidator()
        {
            RuleFor(c => c.OrderId).NotEmpty().WithMessage("OrderId is required.");
        }
}
}
