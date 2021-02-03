using DeliVeggie.DAC.DO;
using DeliVeggie.DAC.Repositories.Products;
using DeliVeggie.Domain.DTO;
using DeliVeggie.MQClient.MQRequester;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeliVeggie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private IProductRepository _productRepository;
        private IMQProductsRequestor _mqProductsRequestor;


        public ProductsController(IMQProductsRequestor mqProductsRequestor, IProductRepository productRepository)
        {
            _mqProductsRequestor = mqProductsRequestor;
            _productRepository = productRepository;
        }


        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return await _mqProductsRequestor.GetProducts();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product =  await _productRepository.Get(id);

            return new JsonResult(product);
        }

        //// POST api/<ProductsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ProductsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
