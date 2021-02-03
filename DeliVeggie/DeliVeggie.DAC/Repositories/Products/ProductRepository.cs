using AutoMapper;
using DeliVeggie.DAC.DO;
using DeliVeggie.Domain.DTO;
using DeliVeggie.Domain.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliVeggie.DAC.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {


        private readonly IMongoCollection<Product> _productsCollection;
        private readonly IMongoCollection<PriceReduction> _priceReductionCollection;

        private readonly IMapper _mapper;

        public ProductRepository(MongoDBDatabaseSettings settings, IMapper mapper)
        {
            IMongoClient mongoClient = new MongoClient(settings.ConnectionString);
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            _productsCollection = database.GetCollection<Product>(nameof(Product));
            _priceReductionCollection = database.GetCollection<PriceReduction>(nameof(PriceReduction));

            _mapper = mapper;
        }

        public async Task<ObjectId> Create(Product product)
        {
            await _productsCollection.InsertOneAsync(product);

            return product.Id;
        }

        public async Task<ProductDTO> Get(string objectIdAsString)
        {

            var objectId = ObjectId.Parse(objectIdAsString);
            var filter = Builders<Product>.Filter.Eq(c => c.Id, objectId);
            var product = await _productsCollection.Find(filter).FirstOrDefaultAsync();




            ProductDTO productDTO = _mapper.Map<ProductDTO>(product);

            var priceReductions = await _priceReductionCollection.Find(_ => true).ToListAsync();
            var reduction = priceReductions.FirstOrDefault(r => r.DayOfWeek == (int)productDTO.EntryDate.DayOfWeek);
            if (reduction != null)
            {
                var newPrice = product.Price * (1.0 - reduction.Reduction);

                // description with normal has to be set first. Because of the price change!!!
                productDTO.Description = "It' a bargain on " + product.EntryDate.DayOfWeek + ". You get a " + (reduction.Reduction * 100).ToString() + "% reduction. Normal price would be " + product.Price.ToString();
                productDTO.Price = newPrice;
            }

            return productDTO;
        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            var products = await _productsCollection.Find(_ => true).ToListAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }




        public async Task Seed()
        {

            var products = await Get();

            if(products != null && products.ToList().Count == 0)
            {
                for (int i = 1; i < 9; i++)
                {
                    Product product = new Product { Name = "Product_" + i, Description = "Description_" + 1, EntryDate = DateTime.Now.AddDays(- i), Price = Math.Round(i * 10.2, 2) };
                    await Create(product);
                }

            }

        }
    }
}
