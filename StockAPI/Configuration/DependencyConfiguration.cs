using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using StockAPI.Consumers;
using StockAPI.DataStructures;

namespace StockAPI.Configuration;

public static class DependencyConfiguration
{
    public static IServiceCollection AddMassTransitCfg(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<OrderCreatedEventConsumer>();

            configurator.UsingRabbitMq((context, busConfigurator) =>
                {
                    Console.WriteLine($" order  api: configuration[\"RabbitMqConn\"] {configuration["RabbitMqConn"]}");
                    
                    configurator.UsingRabbitMq((context, busConfigurator) =>
                        busConfigurator.Host(configuration["RabbitMqConn"]));

                    busConfigurator.ReceiveEndpoint(RabbitMqConfig.Stock_OrderCreatedEventQueue,
                        e => e.ConfigureConsumer<OrderCreatedEventConsumer>(context));
                }
            );
        });

        return services;
    }

    public static IServiceCollection AddDbConnCfg(this IServiceCollection services)
    {
        services.AddDbContext<StockAPIDbContext>(options =>
        {
            string connectionString = Environment.GetEnvironmentVariable("STOCK_DB_CONN");
            Console.WriteLine("stock api STOCK_DB_CONN:", connectionString);

            options.UseNpgsql(Environment.GetEnvironmentVariable("STOCK_DB_CONN"));
        });

        return services;
    }
}