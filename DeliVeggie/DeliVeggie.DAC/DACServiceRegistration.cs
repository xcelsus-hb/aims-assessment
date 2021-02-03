using AutoMapper;
using DeliVeggie.DAC.Repositories.Products;
using DeliVeggie.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DeliVeggie.DAC
{
    public static class DACServiceRegistration
    {
        public static IServiceCollection AddDACServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<MongoDBDatabaseSettings>(
            configuration.GetSection(nameof(MongoDBDatabaseSettings)));


            services.AddSingleton<MongoDBDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDBDatabaseSettings>>().Value);

            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPriceReductionRepository, PriceReductionRepository>();

            return services;
        }


    }
}
