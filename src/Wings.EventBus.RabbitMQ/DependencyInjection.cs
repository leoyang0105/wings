﻿using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Wings.EventBus.RabbitMQ
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWingsEventBusRabbitMQ(this IServiceCollection services, RabbitMQConfiguration mqConfig, Assembly assembly)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumers(assembly);
                config.UsingRabbitMq((ctx, mq) =>
                {
                    mq.Host(mqConfig.HostName, mqConfig.VirtualHost, h =>
                    {
                        h.Username(mqConfig.Username);
                        h.Password(mqConfig.Password);

                    });
                    mq.ReceiveEndpoint(mqConfig.QueueName, x =>
                    {
                        x.ConfigureConsumers(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService(true);
            return services;
        }
    }
}
