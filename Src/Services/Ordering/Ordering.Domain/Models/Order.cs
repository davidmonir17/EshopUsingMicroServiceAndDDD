

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public CustomerId CustomerId { get; private set; } = default;
        public OrderName OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatues Statues { get; private set; } = OrderStatues.pending;

        public decimal TotalPrice
        {
            get => _orderItems.Sum(item => item.Price * item.Quantity);
            private set { } // This property is read-only, it should not be set directly.
        }
        public static Order Create(
            OrderId orderId,
            CustomerId customerId,
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment)
        {


            var order = new Order
            {
                Id = orderId,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Statues = OrderStatues.pending
            };
            order.AddDomainEvent(new OrderCreateEvent(order));
            return order;
        }
        public void Update(OrderName orderName, Address shippingAddress,
            Address billingAddress,
            Payment payment, OrderStatues orderStatues)
        {


            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Statues = orderStatues;

            AddDomainEvent(new OrderUpdateEvent(this));
        }
        public void AddOrderItem(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

            var orderItem = new OrderItem(Id, productId, quantity, price);
            _orderItems.Add(orderItem);

            //AddDomainEvent(new OrderItemAddedEvent(this, orderItem));
        }
        public void RemoveOrderItem(ProductId productId)
        {
            var orderItem = _orderItems.FirstOrDefault(item => item.ProductId == productId);
            if (orderItem == null)
            {
                throw new InvalidOperationException($"Order item with ID {productId} not found.");
            }

            _orderItems.Remove(orderItem);
            // AddDomainEvent(new OrderItemRemovedEvent(this, orderItem));
        }
    }
}
