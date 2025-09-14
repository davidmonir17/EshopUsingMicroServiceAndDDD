

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandller(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            //create order entity from commandobject
            //save to database
            //return order id
            var order = CreateNewOrder(command.Order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value);
            throw new NotImplementedException();
        }
        private Order CreateNewOrder(OrderDTO orderDTO)
        {
            var shippingAddress = Address.Of(
                orderDTO.ShippingAddress.FirstName,
                orderDTO.ShippingAddress.LastName,
                orderDTO.ShippingAddress.EmailAddress,
                orderDTO.ShippingAddress.AddressLine,
                orderDTO.ShippingAddress.Country,
                orderDTO.ShippingAddress.State,
                orderDTO.ShippingAddress.ZipCode
               );
            var BillingAddress = Address.Of(
                orderDTO.BillingAddress.FirstName,
                orderDTO.BillingAddress.LastName,
                orderDTO.BillingAddress.EmailAddress,
                orderDTO.BillingAddress.AddressLine,
                orderDTO.BillingAddress.Country,
                orderDTO.BillingAddress.State,
                orderDTO.BillingAddress.ZipCode
               );
            var payment = Payment.Of(
                orderDTO.Payment.CardNumber,
                orderDTO.Payment.CardName,
                orderDTO.Payment.Expiration,
                orderDTO.Payment.Cvv,
                orderDTO.Payment.PaymentMethod
                );
            var newOrder = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(orderDTO.CustomerId),
                OrderName.Of(orderDTO.OrderName),
                shippingAddress, BillingAddress, payment
                );
            foreach (var item in orderDTO.OrderItems)
            {
                newOrder.AddOrderItem(ProductId.Of(item.ProductId),
                    item.Quantity,
                    item.Price
                    );
            }
            return newOrder;


        }
    }
}
