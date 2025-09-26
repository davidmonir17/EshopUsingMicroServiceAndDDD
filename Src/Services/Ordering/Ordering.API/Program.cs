using Ordering.API;
using Ordering.Application;
using Ordering.Infrastracture;
using Ordering.Infrastracture.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();
if(app.Environment.IsDevelopment())
{
  await app.InitDatabaseAsync();
}
app.Run();
