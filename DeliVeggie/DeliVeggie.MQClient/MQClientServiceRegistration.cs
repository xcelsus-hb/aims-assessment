using DeliVeggie.Domain.MQRequester;
using DeliVeggie.MQClient.MQRequester;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace DeliVeggie.MQClient
{
    public static class MQClientServiceRegistration
    {

        public static IServiceCollection AddMQClientServices(this IServiceCollection services, IConfiguration configuration)
        {



            services.Configure<RabbitMQSettings>(
            configuration.GetSection(nameof(RabbitMQSettings)));


            services.AddSingleton<RabbitMQSettings>(sp =>
                sp.GetRequiredService<IOptions<RabbitMQSettings>>().Value);


            services.AddScoped<IMQProductsRequestor, MQProductsRequestor>();


            return services;
        }
    }
}
