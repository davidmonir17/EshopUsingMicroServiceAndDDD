using Ordering.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrderByNameHandller(IApplicationDbContext dbContext) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
    {
        public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery Query, CancellationToken cancellationToken)
        {
            var order= await dbContext.Orders.Include(o=>o.OrderItems).AsNoTracking().Where(o=>o.OrderName.Value.Contains(Query.Name)).OrderBy(o=>o.OrderName).ToListAsync(cancellationToken);

            return new GetOrderByNameResult(order.ToOdrderDtosList());

        }

        //private List<OrderDTO> projectToOrderDTOs(IEnumerable<Order> orders)
        //{
        //    List<OrderDTO> result = new();
        //    foreach(var order in orders)
        //    {
        //        var orderDto = new OrderDTO(order.Id.Value,
        //            order.CustomerId.Value, order.OrderName.Value
        //            , new AddressingDTO(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.state, order.ShippingAddress.ZipCode),
        //             new AddressingDTO(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.state, order.BillingAddress.ZipCode),
        //             new PaymentDTO(order.Payment.CardName, order.Payment.CardName, order.Payment.Expiration, order.Payment.Cvv, order.Payment.PaymentMethod),
        //             order.Statues, order.OrderItems.Select(oi => new OrderItemDTO(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList());
        //        result.Add(orderDto);
        //    }
        //    return result;

        //}
    }
}
