using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastracture
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OrderingDb");
            // Register infrastructure services here
            // e.g., services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
