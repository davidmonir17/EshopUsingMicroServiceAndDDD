using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandller(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand Command, CancellationToken cancellationToken)
        {
            var orderId= OrderId.Of(Command.Order.Id);
            var order = await dbContext.Orders.FindAsync(orderId, cancellationToken);
            if (order == null)
            {
                throw new OrderNotFoundException(Command.Order.Id);

            }
            UpdateOrderWithNewValues(order, Command.Order);
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValues(Order order, OrderDTO orderDTO)
        {
            var UpdatedshippingAddress = Address.Of(
                orderDTO.ShippingAddress.FirstName,
                orderDTO.ShippingAddress.LastName,
                orderDTO.ShippingAddress.EmailAddress,
                orderDTO.ShippingAddress.AddressLine,
                orderDTO.ShippingAddress.Country,
                orderDTO.ShippingAddress.State,
                orderDTO.ShippingAddress.ZipCode
               );
            var UpdatedBillingAddress = Address.Of(
                orderDTO.BillingAddress.FirstName,
                orderDTO.BillingAddress.LastName,
                orderDTO.BillingAddress.EmailAddress,
                orderDTO.BillingAddress.AddressLine,
                orderDTO.BillingAddress.Country,
                orderDTO.BillingAddress.State,
                orderDTO.BillingAddress.ZipCode
               );
            var Updatedpayment = Payment.Of(
                orderDTO.Payment.CardNumber,
                orderDTO.Payment.CardName,
                orderDTO.Payment.Expiration,
                orderDTO.Payment.Cvv,
                orderDTO.Payment.PaymentMethod
                );
            order.Update(
                OrderName.Of(orderDTO.OrderName),
                UpdatedshippingAddress,
                UpdatedBillingAddress,
                Updatedpayment,
                orderDTO.Status // Assuming statues is not updated in this operation
                );
        }
    }
}
