using Employees.Data;
using Employees.Interfaces;
using Employees.Services;
using Microsoft.EntityFrameworkCore;

namespace Employees.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                );
            });

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });

        public static void ConfigureCaching(this IServiceCollection services) =>
            services.AddResponseCaching();

        public static void ConfigureCacheHeaders(this IServiceCollection services) =>
            services.AddHttpCacheHeaders(
                (expiration) =>
                {
                    expiration.MaxAge = 180;
                },
                (validation) =>
                {
                    validation.MustRevalidate = true;
                }
            );
    }
}
