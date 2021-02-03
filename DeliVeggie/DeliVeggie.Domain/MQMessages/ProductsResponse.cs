using DeliVeggie.Domain.DTO;
using System.Collections.Generic;

namespace DeliVeggie.Domain.MQMessages
{
    public class ProductsResponse
    {
        public ProductsResponse()
        {
            Products = new List<ProductDTO>();
        }
        public List<ProductDTO> Products { get; set; }
    }
}
