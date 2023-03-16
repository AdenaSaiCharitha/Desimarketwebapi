using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesiMarket.Models;
using DesiMarket.Repositories;
using Microsoft.Extensions.Logging;
using log4net.Repository.Hierarchy;

namespace DesiMarket.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly OnlineGroceryDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogError("Invalid id");
                return NotFound();
            }
            _logger.LogInformation("Products fetched successful");
            return Ok(product);
        }
        [HttpPost]
        [Route("Createproduct")]
        public async Task<IActionResult> CreateProduct(Products product)
        {
            await _productRepository.CreateProductAsync(product);
            _logger.LogInformation("ordered successfully");
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Products product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            await _productRepository.UpdateProductAsync(product);
            _logger.LogInformation("updated successful");
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteProductAsync(product);
            _logger.LogInformation("Deleted successfully");
            return Ok();
        }

        //public ProductsController(OnlineGroceryDbContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Products
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Products>>> GetProduct()
        //{
        //    return await _context.Product.ToListAsync();
        //}

        //// GET: api/Products/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Products>> GetProducts(int id)
        //{
        //    var products = await _context.Product.FindAsync(id);

        //    if (products == null)
        //    {
        //        return NotFound();
        //    }

        //    return products;
        //}

        //// PUT: api/Products/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProducts(int id, Products products)
        //{
        //    if (id != products.ProductId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(products).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Products
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Products>> PostProducts(Products products)
        //{
        //    _context.Product.Add(products);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProducts", new { id = products.ProductId }, products);
        //}

        //// DELETE: api/Products/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Products>> DeleteProducts(int id)
        //{
        //    var products = await _context.Product.FindAsync(id);
        //    if (products == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Product.Remove(products);
        //    await _context.SaveChangesAsync();

        //    return products;
        //}

        //private bool ProductsExists(int id)
        //{
        //    return _context.Product.Any(e => e.ProductId == id);
        //}
    }
}
