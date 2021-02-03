using DeliVeggie.DAC.Repositories.Products;
using DeliVeggie.Domain.DTO;
using DeliVeggie.Domain.MQMessages;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliVeggie.PriceAdjuster
{
    public class App
    {

        private IProductRepository _productsRepository;
        private IPriceReductionRepository _priceReductionRepository;
        public App(IProductRepository productsRepository, IPriceReductionRepository priceReductionRepository)
        {
            _productsRepository = productsRepository;
            _priceReductionRepository = priceReductionRepository;
        }




        public void Run()
        {
            for (int i = 0; i < 10; i++)
            {
                using (var bus = RabbitHutch.CreateBus("host=localhost;username=rmquser;password=xcelsus4132"))
                {
                    bus.Rpc.Respond<ProductsRequest, ProductsResponse>(Responder);


                    Console.WriteLine("Listening for messages. Hit <return> to quit.");
                    Console.ReadLine();
                }
            }
        }

        public ProductsResponse Responder(ProductsRequest request)
        {

            ProductsResponse resp = new ProductsResponse();

            var produktDTOs = _productsRepository.Get().Result;
            var priceReductions = _priceReductionRepository.Get().Result.ToList(); ;


            foreach (var product in produktDTOs)
            {
                var reduction = priceReductions.FirstOrDefault(r => r.DayOfWeek == (int)product.EntryDate.DayOfWeek);
                if(reduction != null)
                {
                    var newPrice = product.Price * (1.0 - reduction.Reduction);

                    // description with normal has to be set first. Because of the price change!!!
                    product.Description = "It' a bargain on " + product.EntryDate.DayOfWeek + ". You get a " +  (reduction.Reduction * 100).ToString() + "% reduction. Normal price would be " + product.Price.ToString();
                    product.Price = newPrice;
                }

                resp.Products.Add(product);
            }


            return resp;
        }


    }
}
