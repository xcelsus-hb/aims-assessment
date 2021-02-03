using DeliVeggie.DAC.DO;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliVeggie.DAC.Repositories.Products
{
    public interface IPriceReductionRepository
    {
        Task<ObjectId> Create(PriceReduction priceReduction);
        Task<IEnumerable<PriceReduction>> Get();
        Task Seed();
    }
}