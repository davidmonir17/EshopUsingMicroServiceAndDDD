using MassTransit;
using MassTransit.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandller.Domain
{
    internal class OrderCreateEventHandller(IPublishEndpoint publishEndpoint,ILogger<OrderCreateEventHandller> logger) : INotificationHandler<OrderCreateEvent>
    {
        public async Task Handle(OrderCreateEvent domainevent, CancellationToken cancellationToken)
        {
          logger.LogInformation("Domain Event: {DomainEvent}", domainevent.GetType().Name);
            var ordercreatedIntegration = domainevent.Order.ToOrderDto();
            await publishEndpoint.Publish(ordercreatedIntegration,cancellationToken);
           
        }
    }
}
