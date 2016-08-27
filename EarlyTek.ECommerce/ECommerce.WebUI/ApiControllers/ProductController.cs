using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ECommerce.Domain.Entities;
using ECommerce.Repository.Abstract;

namespace ECommerce.WebUI.ApiControllers
{
    public class ProductController : ApiController
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository prodRepository)
        {
            productRepository = prodRepository;
        }

        // GET api/product
        public IEnumerable<Product> Get()
        {
            return productRepository.Products;
        }

        // GET api/product/5
        public Product Get(int id)
        {
            return productRepository.Products.Single(p => p.ProductId == id);
        }

        // POST api/product
        public void Post([FromBody]string value)
        {
        }

        // PUT api/product/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/product/5
        public void Delete(int id)
        {
        }
    }
}
