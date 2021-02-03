using DeliVeggie.DAC.DO;
using DeliVeggie.Domain.DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeliVeggie.DAC.Repositories.Products
{
    public interface IProductRepository
    {
        // Create - Needed for seeding at the moment
        Task<ObjectId> Create(Product product);

        // Read
        Task<ProductDTO> Get(string objectId);
        Task<IEnumerable<ProductDTO>> Get();
        //IEnumerable<Product> Get();
        //Task<IEnumerable<Product>> GetByCategory(string cat);

        // Update - Not needed at the moment
        // Task<bool> Update(ObjectId objectId, Product car);

        // Delete - Not needed at the moment
        // Task<bool> Delete(ObjectId objectId);

        // Seed
        Task Seed();

    }
}
