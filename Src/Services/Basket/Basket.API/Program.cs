using BuldingBlocks.Exceptions.Handler;
using Discount.Grpc;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(BuldingBlocks.Behaviors.ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(BuldingBlocks.Behaviors.LoggingBehavior<,>));
});
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("BasketDb"));
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);

}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

//example if not install scruted liblrary for decorator pattern 
//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//    var basketRepository = provider.GetRequiredService<BasketRepository>();
//    return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
//});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddGrpcClient<DiscountService.DiscountServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountURL"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return handler;
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(opt => { });

app.Run();
