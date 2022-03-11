using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using Wings.Host.Services;

namespace Wings.Host.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddWingsControllers(this IServiceCollection services)
        {
            return services.AddControllers()
                 .AddNewtonsoftJson(setup =>
                 {
                     setup.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 });
        }
        public static IServiceCollection AddWingsHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();

            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                options.Predicate = (check) => check.Tags.Contains("ready");
            });
            return services;
        }
        public static IServiceCollection AddWingsHttpUserContext(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserContext, UserContext>();
            return services;
        }
        public static IServiceCollection AddWingsAuth(this IServiceCollection services, [NotNull] string apiName, [NotNull] string identityUrl)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = identityUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = apiName;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            return services;
        }
        public static IServiceCollection AddWingsSwagger(this IServiceCollection services, string apiName, string apiVersion = "v1")
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(apiVersion, new OpenApiInfo { Title = apiName, Version = apiVersion });
            });
        }
        public static IServiceCollection AddWingsAllowAnyCorsPolicy(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(HostDefaults.AllowAnyCorsPolicy,
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
    }
}
