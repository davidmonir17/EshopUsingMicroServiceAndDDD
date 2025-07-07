using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            // e.g., services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
