using BuldingBlocks.Exceptions.Handler;
using BuldingBlocks.Pagination;
using Carter;
using HealthChecks.UI.Client;
using Mapster;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Ordering.API.EndPoints;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services,IConfiguration configuration)
        {
            // Register API services here
            // e.g., services.AddControllers();
            // services.AddSwaggerGen();
            services.AddCarter();

            services.AddExceptionHandler<CustomExceptionHandler>();
            // Mapster configuration for GetOrdersResult to GetOrdersResponse
            TypeAdapterConfig<GetOrdersResult, GetOrdersResponse>.NewConfig()
                .Map(dest => dest.orders, src => src.orders);

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
            return services;
        }
        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure API middleware here
            // e.g., app.UseSwagger();
            // app.UseAuthorization();
            // app.MapControllers();
            app.MapCarter();
            app.UseExceptionHandler(options => { });
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
                });
            return app;
        }
    }
}
