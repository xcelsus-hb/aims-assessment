using DeliVeggie.DAC;
using DeliVeggie.DAC.Repositories.Products;
using DeliVeggie.Domain.DTO;
using DeliVeggie.Domain.MQMessages;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace DeliVeggie.PriceAdjuster
{
    class Program
    {



 


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World from DeliVeggie.PriceAdjuster!");

            Setup();
        }


        static void Setup()
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<IProductRepository>().Seed();
            serviceProvider.GetService<IPriceReductionRepository>().Seed();
            
            serviceProvider.GetService<App>().Run();
            
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);
            
            services.AddDACServices(config);

            // to run the application
            services.AddTransient<App>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
