using BuildingBlocks.messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandller.Integration
{
    public class BasketCheckoutEventHandller (ISender sender,ILogger<BasketCheckoutEventHandller> logger): IConsumer<BasketCheckOutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckOutEvent> context)
        {
            logger.LogInformation("Integration event handler :{IntegrationEvent}",context.Message.GetType().Name);
            var command= MapToCreateOrderCommand(context.Message);
            await sender.Send(command);

        }

        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckOutEvent message)
        {
            var addressDTO=new AddressingDTO(message.FirstName, message.LastName,message.Email,message.AddressLine,message.Country,message.State,message.ZipCode);
            var paymentDTO = new PaymentDTO(message.CardNumber, message.CardName, message.Expiration, message.Cvv, message.PaymentMethod);
            var orderId = Guid.NewGuid();
            var orderDto= new OrderDTO(orderId,
                message.CustomerId,message.UserName,addressDTO,addressDTO,paymentDTO,Ordering.Domain.Enums.OrderStatues.pending,
                [
                    new OrderItemDTO(orderId,new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),2,500),
                    new OrderItemDTO(orderId,new Guid("c9d4c053-49b6-410c-bc78-2d54a9991872"),2,900),
                    
                ]
                );
            return new CreateOrderCommand(orderDto);
        }

    }

}
