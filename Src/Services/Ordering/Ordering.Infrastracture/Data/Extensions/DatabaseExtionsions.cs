using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastracture.Data.Extensions
{
    public static class DatabaseExtionsions
    {
        public static async Task InitDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApllicationDbContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedAsync(context);
        }
        private static async Task SeedAsync(ApllicationDbContext context)
        {
            await SeedCustomerAsync(context);
            await SeedProductAsync(context);
            await SeedOrderandItemsAsync(context);

        }
        private static async Task SeedCustomerAsync(ApllicationDbContext context)
        {
            if (! await context.Customers.AnyAsync())
            {
               await context.Customers.AddRangeAsync(InitialData.customers);
                await context.SaveChangesAsync();
            } 
        } 
        private static async Task SeedProductAsync(ApllicationDbContext context)
        {
            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(InitialData.products);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedOrderandItemsAsync(ApllicationDbContext context)
        {
            if (!await context.Orders.AnyAsync())
            {
                await context.Orders.AddRangeAsync(InitialData.ordersWithItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
