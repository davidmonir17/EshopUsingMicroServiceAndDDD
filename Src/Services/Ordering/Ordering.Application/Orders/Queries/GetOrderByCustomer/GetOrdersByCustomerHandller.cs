using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    public class GetOrdersByCustomerHandller(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders.Include(o=>o.OrderItems).AsNoTracking().Where(o=>o.CustomerId==CustomerId.Of( query.customerId)).ToListAsync(cancellationToken);
            return new GetOrdersByCustomerResult(orders.ToOdrderDtosList());

        }
    }
}
