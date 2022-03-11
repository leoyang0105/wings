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
                cfg.UsingInMemory((ctx, cfg) =>
                {
                    cfg.TransportConcurrencyLimit = 100;
                    cfg.ConfigureEndpoints(ctx);
                });
            });
            services.AddMassTransitHostedService(true);
            return services;
        }
    }
}
