using MediatR;
using ProductStore.Api.Cache;
using ProductStore.Api.Client;
using ProductStore.Api.Domain.Behaviors;
using ProductStore.Api.Domain.Contracts;
using ProductStore.Api.Repository;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Configuration
{
    /// <summary>
    /// Custom dependency injection class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        /// <summary>
        /// Allows to inject the necessary dependencies as IoC
        /// </summary>
        /// <param name="services">Services of the application</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <returns>Services with added dependencies</returns>
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.Configure<DiscountClientConfiguration>(configuration.GetSection("DiscountApi"));
            services.AddScoped<IDiscountClient, DiscountClient>();

            services.AddScoped<ICacheService, StatusCacheService>();

            return services;
        }
    }
}
