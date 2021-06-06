using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wings.Caching
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWingsMemoryCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheConfig>(configuration.GetSection(CacheConfig.CONFIGURATION_KEY));
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            return services;
        }
    }
}
