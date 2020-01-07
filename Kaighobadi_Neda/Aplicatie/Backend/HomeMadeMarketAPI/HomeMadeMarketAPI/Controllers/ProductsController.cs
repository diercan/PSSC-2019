using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMadeMarketAPI.Models;
using HomeMadeMarketAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeMadeMarketAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService service)
        {
            _productService = service;
        }

        // GET: api/Products
        //[HttpGet]
        //public ActionResult<List<Product>> Get() => _productService.Get();
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id:length(24)}", Name = "GetProducts")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        //GET: api/Products/sellerId
        [HttpGet]
        public ActionResult<List<Product>> GetBySeller(string sellerId) => _productService.GetBySeller(sellerId);

        //GET: api/Products/category
        [HttpGet]
        public ActionResult<List<Product>> GetByCategory(string category) => _productService.GetByCategory(category);


        // PUT: api/UserLogins/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public IActionResult Update(Product productIn)
        {
            var product = _productService.Get(productIn.Id);
            if (product == null)
            {
                return NotFound();
            }
            _productService.Update(productIn.Id, productIn);
            return NoContent();
        }

        // POST: api/UserLogins
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            _productService.Create(product);

            return CreatedAtRoute("GetProducts", new { id = product.Id.ToString() }, product);
        }

        // DELETE: api/UserLogins/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.Remove(product.Id);

            return NoContent();
        }
    }
}
