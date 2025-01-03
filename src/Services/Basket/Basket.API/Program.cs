using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
//Add services to container
var assembly = typeof(Program).Assembly;
var connectionString = builder.Configuration.GetConnectionString("Database")!;
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(assembly);
    options.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    options.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();
builder.Services.AddCarter();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();
app.UseExceptionHandler(options => { });
//Configure the HTTP request pipeline
app.MapCarter();
app.Run();
