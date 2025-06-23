using BuldingBlocks.Exceptions.Handler;
using Catalog.API.Data;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(BuldingBlocks.Behaviors.ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(BuldingBlocks.Behaviors.LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("CatalogDb"));
}).UseLightweightSessions();
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitData>();


builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks();
var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(opt => { });
app.Run();
