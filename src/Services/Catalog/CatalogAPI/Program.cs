using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

//Add services to container
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(assembly);
    options.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    // options.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddCarter();
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter();

app.Run();