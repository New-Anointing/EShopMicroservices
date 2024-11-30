using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add services to container
var assembly = typeof(Program).Assembly;
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(assembly);
    options.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    options.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddCarter();
builder.Services.AddMarten(options =>
{
    options.Connection(connectionString!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString!);

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter();
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.UseExceptionHandler(options => { });

app.Run();