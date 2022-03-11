using Microsoft.AspNetCore.Builder;

namespace Wings.Host.Extensions
{
    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseWingstAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
        public static IApplicationBuilder UseWingstAllowAnyCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(HostDefaults.AllowAnyCorsPolicy);
            return app;
        }
        public static IApplicationBuilder UseWingsSwagger(this IApplicationBuilder app, string apiName, string apiVersion, string pathBase = null)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{(string.IsNullOrEmpty(pathBase) ? string.Empty : pathBase)}/swagger/{apiVersion}/swagger.json", $"{apiName} {apiVersion}");
            });
            return app;
        }
    }
}
