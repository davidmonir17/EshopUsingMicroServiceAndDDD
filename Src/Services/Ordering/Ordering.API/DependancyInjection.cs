using Carter;

namespace Ordering.API
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Register API services here
            // e.g., services.AddControllers();
            // services.AddSwaggerGen();
            services.AddCarter();

            return services;
        }
        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure API middleware here
            // e.g., app.UseSwagger();
            // app.UseAuthorization();
            // app.MapControllers();
            app.MapCarter();
            return app;
        }
    }
}
