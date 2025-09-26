using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extensions
{
    public static class OrderExtension
    {
        public static IEnumerable<OrderDTO> ToOdrderDtosList(this IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderDTO(
                order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                new AddressingDTO(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.state, order.ShippingAddress.ZipCode),
                     new AddressingDTO(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.state, order.BillingAddress.ZipCode),
                     new PaymentDTO(order.Payment.CardNumber, order.Payment.CardName, order.Payment.Expiration, order.Payment.Cvv, order.Payment.PaymentMethod),
                     order.Statues,
                     order.OrderItems.Select(oi => new OrderItemDTO(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                ));
        }

        public static OrderDTO ToOrderDto(this Order orders)
        {
           return DTOFromOrder(orders);
        }

        private static OrderDTO DTOFromOrder(Order order)
        {
            return new OrderDTO(order.Id.Value,
                order.CustomerId.Value, order.OrderName.Value,
                new AddressingDTO(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.state, order.ShippingAddress.ZipCode)
                ,new AddressingDTO(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.state, order.BillingAddress.ZipCode)
                , new PaymentDTO(order.Payment.CardNumber,order.Payment.CardName,order.Payment.Expiration,order.Payment.Cvv,order.Payment.PaymentMethod)
                , order.Statues,
                order.OrderItems.Select(oi => new OrderItemDTO(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                );
        }
    }
}
