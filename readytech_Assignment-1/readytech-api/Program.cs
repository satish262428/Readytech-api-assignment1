
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = ConfigurationOptions.Parse("localhost:6379");
            return ConnectionMultiplexer.Connect(configuration);
        });

// Register Redis database
builder.Services.AddSingleton<IDatabase>(sp =>
{
    var connectionMultiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
    return connectionMultiplexer.GetDatabase();
});

// Register services
builder.Services.AddScoped<ICallCountService, CallCountService>();


// Register handlers
builder.Services.AddScoped<IRequestHandler, AprilFirstHandler>();
builder.Services.AddScoped<IRequestHandler, FifthCallHandler>();
builder.Services.AddScoped<IRequestHandler, DefaultHandler>();

// Set up the chain during service registration
var serviceProvider = builder.Services.BuildServiceProvider();
var handlers = serviceProvider.GetServices<IRequestHandler>().ToArray();

for (int i = 0; i < handlers.Length - 1; i++)
{
    handlers[i].SetSuccessor(handlers[i + 1]);
}

var handlerChain = handlers.First();

// Register the handler chain as a singleton
builder.Services.AddSingleton<IRequestHandler>(handlerChain);

builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin()  // Allow all origins
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.MapControllers();


app.Run();
