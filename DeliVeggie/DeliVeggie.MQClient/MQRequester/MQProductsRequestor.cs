using DeliVeggie.Domain.DTO;
using DeliVeggie.Domain.MQMessages;
using DeliVeggie.Domain.MQRequester;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliVeggie.MQClient.MQRequester
{
    public class MQProductsRequestor : IMQProductsRequestor
    {

        private RabbitMQSettings _settings;

        public MQProductsRequestor(RabbitMQSettings settings)
        {
            _settings = settings;
        }

        public async Task<List<ProductDTO>> GetProducts()
        {

            List<ProductDTO> products = new List<ProductDTO>();

            using (var bus = RabbitHutch.CreateBus(_settings.ConnectionString))
            {

                var req = new ProductsRequest();

                await bus.Rpc.RequestAsync<ProductsRequest, ProductsResponse>(req).ContinueWith(response =>
                {

                    products = response.Result.Products;
                    Console.WriteLine("Got response");

                });

            }

            return products;

        }
    }
}
