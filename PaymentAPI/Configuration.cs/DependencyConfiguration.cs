using MassTransit;
using PaymentAPI.Consumers;

namespace PaymentAPI.Configuration.cs;

public static class DependencyConfiguration
{
    public static IServiceCollection AddMassTransitCfg(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<StockReservedEventConsumer>();

            Console.WriteLine($" paymetn api: configuration[\"RabbitMqConn\"] {configuration["RabbitMqConn"]}");

            cfg.UsingRabbitMq((context, busConfigurator) => busConfigurator.Host(configuration["RabbitMqConn"]));
        });

        return services;
    }
}