using System.Reflection;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using StockAPI.Consumers;
using StockAPI.DataStructures;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<OrderCreatedEventConsumer>();

    configurator.UsingRabbitMq((context, busConfigurator) =>
        {
            busConfigurator.Host(
                "amqps://fskgqixc:r_C4CG8NFB78dEF175g_PRA7Drcbguj3@moose.rmq.cloudamqp.com/fskgqixc"); //ENV'YE EKLENECEK.

            busConfigurator.ReceiveEndpoint(RabbitMqConfig.Stock_OrderCreatedEventQueue,
                e => e.ConfigureConsumer<OrderCreatedEventConsumer>(context));
        }
    );
});

builder.Services.AddDbContext<StockAPIDbContext>(options =>
{
    string connectionString = Environment.GetEnvironmentVariable("STOCK_DB_CONN");
    Console.WriteLine("conn string:", connectionString);

    options.UseNpgsql("Host=localhost;Port=5432;Database=StockDB;Username=postgres;Password=Password2;");
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("v1/swagger.json", "StockAPI"); });

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();