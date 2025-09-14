using Ordering.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.DTOs
{
   public record OrderDTO(
       Guid Id,
       Guid CustomerId,
       string OrderName,
       AddressingDTO ShippingAddress,
       AddressingDTO BillingAddress,
       PaymentDTO Payment,
       OrderStatues Status,
       List<OrderItemDTO> OrderItems

       );
}
