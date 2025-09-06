using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastracture.Data;

namespace Ordering.Infrastracture
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OrderingConnectionString");
            // Register infrastructure services here
            services.AddDbContext<ApllicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            // e.g., services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
