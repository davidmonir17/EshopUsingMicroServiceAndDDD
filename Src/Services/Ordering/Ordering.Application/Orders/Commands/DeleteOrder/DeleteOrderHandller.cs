using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandller(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand Command, CancellationToken cancellationToken)
        {
           var orderId= OrderId.Of(Command.OrderId);
            var order= await dbContext.Orders.FindAsync(orderId,cancellationToken);
            if (order == null)
            {
                throw new OrderNotFoundException(Command.OrderId);

            }
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResult(true);
        }
    }
}
