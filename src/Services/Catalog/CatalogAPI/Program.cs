var builder = WebApplication.CreateBuilder(args);

//Add services to container
builder.Services.AddCarter();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter();

app.Run();