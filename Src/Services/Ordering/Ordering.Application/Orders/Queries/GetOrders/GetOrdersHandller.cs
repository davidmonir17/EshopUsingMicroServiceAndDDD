using BuldingBlocks.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandller(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.pageIndex;
            var pageSize = query.PaginationRequest.pageSize;
            var totalCount= await dbContext.Orders.LongCountAsync(cancellationToken);
            var orders= await dbContext.Orders.Include(o=>o.OrderItems)
                .Skip(pageIndex*pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(new PaginationResult<OrderDTO>(pageIndex, pageSize, totalCount, orders.ToOdrderDtosList()));

        }
    }
}
