using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastracture.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> customers => new List<Customer>
        {
              Customer.Create(CustomerId.Of(Guid.Parse("78a45e94-5bfd-456a-a971-650144db051a")), "David Mon","DHanna@ejada.com" ),
              Customer.Create(CustomerId.Of(Guid.Parse("f97a9f10-6d86-44a5-91a3-7e90c275c88b")), "Andre Mon","Andrew@ejada.com" ),
        };
        public static IEnumerable<Product> products => new List<Product>
        {
              Product.Create(ProductId.Of(Guid.Parse("c9d4c053-49b6-410c-bc78-2d54a9991870")), "IPhone X", 950 ),
              Product.Create(ProductId.Of(Guid.Parse("c9d4c053-49b6-410c-bc78-2d54a9991871")), "Samsung S20", 850 ),
              Product.Create(ProductId.Of(Guid.Parse("c9d4c053-49b6-410c-bc78-2d54a9991872")), "Huawei Plus", 650 ),
        };
        public static IEnumerable<Order> ordersWithItems 
        {
            get
            {
                var address1= Address.Of("mhmet", "Ozkya", "StateA@da.com", "12345", "Egypt","davdsa","38050");
                var address2= Address.Of("dacad", "Ozkya", "StateA@da.com", "545454", "Egypt","davdsa","38050");

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(Guid.Parse("78a45e94-5bfd-456a-a971-650144db051a")),
                    OrderName.Of("Ord_1"),
                    address1,
                    address2,
                    Payment.Of("1234567890123456", "David Mounir","12/25", "123",2)
                    );
                order1.AddOrderItem(ProductId.Of(Guid.Parse("c9d4c053-49b6-410c-bc78-2d54a9991870")), 1, 950);
                order1.AddOrderItem(ProductId.Of(Guid.Parse("c9d4c053-49b6-410c-bc78-2d54a9991871")), 2, 850);
                var order2 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(Guid.Parse("f97a9f10-6d86-44a5-91a3-7e90c275c88b")),
                    OrderName.Of("Ord_2"),
                    address1,
                    address2,
                    Payment.Of("1234567890123456", "Andrew","12/25", "123",2)
                    );
                order2.AddOrderItem(ProductId.Of(Guid.Parse("c9d4c053-49b6-410c-bc78-2d54a9991872")), 1, 650);
                order2.AddOrderItem(ProductId.Of(Guid.Parse("c9d4c053-49b6-410c-bc78-2d54a9991871")), 1, 850);
                return new List<Order> { order1, order2 };
            }    
        }

    }
}
