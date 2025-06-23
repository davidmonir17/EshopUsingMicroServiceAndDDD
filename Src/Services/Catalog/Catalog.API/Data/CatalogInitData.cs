using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync())
            {
                return; // Data already exists, no need to populate
            }
            session.Store<Product>(GetPreConfigProducts());
            await session.SaveChangesAsync();
        }

        public static IEnumerable<Product> GetPreConfigProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "IPhone X",
                    Description = "Description for Product 1",
                    Price = 950.00M,
                    ImageFile="prduct-1",
                    Category = new List<string> { "Smart phone" },
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "samsung 10",
                    Description = "Description for Product 2",
                    Price = 200.99M,
                    ImageFile="prduct-2",
                    Category =new List<string> { "Smart phone" }
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Panasonic Lumix",
                    Description = "Description for Product 3",
                    Price = 250.99M,
                    ImageFile="prduct-3",
                    Category = new List<string> { "Camera" },
                }
            };
        }
    }
}
