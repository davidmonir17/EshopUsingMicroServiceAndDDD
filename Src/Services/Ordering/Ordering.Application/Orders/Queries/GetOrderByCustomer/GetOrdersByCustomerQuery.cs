﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    public record GetOrdersByCustomerQuery(Guid customerId):IQuery<GetOrdersByCustomerResult>;
    public record GetOrdersByCustomerResult(
        IEnumerable<OrderDTO> Orders
        );

}
