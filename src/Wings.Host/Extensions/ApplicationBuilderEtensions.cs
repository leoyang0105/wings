using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Wings.Host.Extensions
{
    public static class ApplicationBuilderEtensions
    {
        public static IApplicationBuilder UseWingstAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
        public static IApplicationBuilder UseWingsSwagger(this IApplicationBuilder app, string apiName, string apiVersion, string pathBase)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{(string.IsNullOrEmpty(pathBase) ? string.Empty : pathBase)}/swagger/{apiVersion}/swagger.json", $"{apiName} {apiVersion}");
            });
            return app;
        }
        public static IApplicationBuilder UseWingsAPI(this IApplicationBuilder app, IConfiguration configuration, IHostEnvironment env)
        {
            var pathBase = configuration["PathBase"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var apiName = configuration["ApiName"];
                if (!string.IsNullOrEmpty(apiName))
                {
                    app.UseWingsSwagger(apiName, configuration["ApiVersion"] ?? "v1", pathBase);
                }
            }
            app.UseRouting();
            app.UseCors(HostDefaults.AllowAnyCorsPolicy);
            app.UseWingstAuth();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            return app;
        }
    }
}
