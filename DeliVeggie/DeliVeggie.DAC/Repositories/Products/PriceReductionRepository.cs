using DeliVeggie.DAC.DO;
using DeliVeggie.Domain.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliVeggie.DAC.Repositories.Products
{
    public class PriceReductionRepository : IPriceReductionRepository
    {

        private readonly IMongoCollection<PriceReduction> _priceReductionsCollection;

        public PriceReductionRepository(MongoDBDatabaseSettings settings)
        {

            IMongoClient mongoClient = new MongoClient(settings.ConnectionString);
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            _priceReductionsCollection = database.GetCollection<PriceReduction>(nameof(PriceReduction));
        }


        public async Task<ObjectId> Create(PriceReduction priceReduction)
        {
            await _priceReductionsCollection.InsertOneAsync(priceReduction);

            return priceReduction.Id;
        }

        public async Task<IEnumerable<PriceReduction>> Get()
        {
            var priceReductions = await _priceReductionsCollection.Find(_ => true).ToListAsync();
            return priceReductions;
        }

        public async Task Seed()
        {


            var priceReductions = await Get();
            if (priceReductions != null && priceReductions.ToList().Count == 0)
            {

                    PriceReduction priceReduction = new PriceReduction { DayOfWeek = 2, Reduction = 0.2 };
                    await Create(priceReduction);


            }


        }

    }
}
