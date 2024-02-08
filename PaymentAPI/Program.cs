using MassTransit;
using PaymentAPI.Consumers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<StockReservedEventConsumer>();

    cfg.UsingRabbitMq((context, busConfigurator) =>
        {
            busConfigurator.Host(
                "amqps://fskgqixc:r_C4CG8NFB78dEF175g_PRA7Drcbguj3@moose.rmq.cloudamqp.com/fskgqixc");
        }
    );
});


var app = builder.Build();

app.Run();