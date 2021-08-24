using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Wings.EventBus
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWingsEventBusInMemory(this IServiceCollection services, Assembly[] assemblies)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumers(assemblies);
                cfg.UsingInMemory((ctx, c) =>
                {
                    c.TransportConcurrencyLimit = 100;
                    c.ReceiveEndpoint(x =>
                    {
                        x.ConfigureConsumers(ctx);
                    });
                });
            });
            return services;
        }
    }
}
