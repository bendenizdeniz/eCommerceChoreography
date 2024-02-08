using System.Reflection;
using eCommerceChoreography.DataStructures;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Consumers;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<PaymentCompletedEventConsumer>();
    configurator.AddConsumer<PaymentFailedEventConsumer>();
    configurator.AddConsumer<StockNotReservedEventConsumer>();

    configurator.UsingRabbitMq((context, busConfigurator) =>
        {
            busConfigurator.Host(
                "amqps://fskgqixc:r_C4CG8NFB78dEF175g_PRA7Drcbguj3@moose.rmq.cloudamqp.com/fskgqixc"); //ENV'YE EKLENECEK.

            busConfigurator.ReceiveEndpoint(RabbitMqConfig.Order_PaymentCompletedEventQueue,
                endpointConfigurator => endpointConfigurator.ConfigureConsumer<PaymentCompletedEventConsumer>(context));

            busConfigurator.ReceiveEndpoint(RabbitMqConfig.Order_PaymentFailedEventQueue,
                endpointConfigurator => endpointConfigurator.ConfigureConsumer<PaymentFailedEventConsumer>(context));

            busConfigurator.ReceiveEndpoint(RabbitMqConfig.Order_StockNotReservedEventQueue,
                endpointConfigurator => endpointConfigurator.ConfigureConsumer<StockNotReservedEventConsumer>(context));
        }
    );
});

builder.Services.AddDbContext<OrderAPIDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("ORDER_DB_CONN")));


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("v1/swagger.json", "OrderAPI"); });

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();