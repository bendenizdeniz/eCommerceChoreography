using System.Reflection;
using eCommerceChoreography.DataStructures;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Consumers;
using Shared;

namespace eCommerceChoreography.Configuration;

public static class DependencyConfiguration
{
    public static IServiceCollection AddMassTransitCfg(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<PaymentCompletedEventConsumer>();
            configurator.AddConsumer<PaymentFailedEventConsumer>();
            configurator.AddConsumer<StockNotReservedEventConsumer>();

            configurator.UsingRabbitMq((context, busConfigurator) =>
                {
                    busConfigurator.Host(configuration["RabbitMqConn"]);

                    busConfigurator.ReceiveEndpoint(RabbitMqConfig.Order_PaymentCompletedEventQueue,
                        endpointConfigurator =>
                            endpointConfigurator.ConfigureConsumer<PaymentCompletedEventConsumer>(context));

                    busConfigurator.ReceiveEndpoint(RabbitMqConfig.Order_PaymentFailedEventQueue,
                        endpointConfigurator =>
                            endpointConfigurator.ConfigureConsumer<PaymentFailedEventConsumer>(context));

                    busConfigurator.ReceiveEndpoint(RabbitMqConfig.Order_StockNotReservedEventQueue,
                        endpointConfigurator =>
                            endpointConfigurator.ConfigureConsumer<StockNotReservedEventConsumer>(context));
                }
            );
        });

        return services;
    }

    public static IServiceCollection AddDbConnectionDfg(this IServiceCollection services)
    {
        Console.WriteLine(
            $"Environment.GetEnvironmentVariable(\"ORDER_DB_CONN\"): {Environment.GetEnvironmentVariable("ORDER_DB_CONN")}");

        services.AddDbContext<OrderAPIDbContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("ORDER_DB_CONN")));

        return services;
    }

    public static IServiceCollection AddMediatRCfg(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}