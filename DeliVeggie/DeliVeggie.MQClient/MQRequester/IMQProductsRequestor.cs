using DeliVeggie.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliVeggie.MQClient.MQRequester
{
    public interface IMQProductsRequestor
    {
        Task<List<ProductDTO>> GetProducts();
    }
}