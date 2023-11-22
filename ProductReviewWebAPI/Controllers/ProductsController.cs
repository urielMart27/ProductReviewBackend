using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductReviewWebAPI.Data;
using ProductReviewWebAPI.DTOs;
using ProductReviewWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReviewWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get: api/Products
        [HttpGet]
        public IActionResult GetPrice([FromQuery] double? maxprice )
        {
            var products = _context.Products.ToList();

            if (maxprice != null)
            {
                products = products.Where(p => p.Price < maxprice).ToList();
            }
            return Ok(products);
        }


        // Get: api/products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products
                .Include(p => p.Reviews)
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Reviews = product.Reviews.Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    Text = r.Text,
                    Rating = r.Rating,

                }).ToList(),
                AverageRating = product.Reviews.Any() ? product.Reviews.Average(r => r.Rating) : 0.0
            };
            return Ok(productDTO);
        }


        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201, product);
        }


        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product updateProduct)
        {
            var currentProduct = _context.Products.Find(id);

            if (currentProduct == null)
            {
                return NotFound();
            }
            currentProduct.Name = updateProduct.Name;
            currentProduct.Price = updateProduct.Price;
            currentProduct.Reviews = updateProduct.Reviews;

            _context.Products.Update(currentProduct);
            _context.SaveChanges();
            return StatusCode(200, currentProduct);
        }


        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
