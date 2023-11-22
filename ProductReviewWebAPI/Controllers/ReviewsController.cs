using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductReviewWebAPI.Data;
using ProductReviewWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReviewWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<ReviewsController>
        [HttpGet]
        public IActionResult Get()
        {
            var reviews = _context.Reviews.ToList();
            return StatusCode(200, reviews);
        }

        // GET api/<ReviewsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            return StatusCode(200, review);
        }

        // POST api/<ReviewsController>
        [HttpPost]
        public IActionResult Post([FromBody] Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return StatusCode(201, review);
        }

        // PUT api/<ReviewsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review updateReview)
        {
            var currentReview = _context.Reviews.Find(id);

            if (currentReview == null)
            {
                return NotFound();
            }
            currentReview.Text = updateReview.Text;
            currentReview.Rating = updateReview.Rating;
            currentReview.ProductId = updateReview.ProductId;
            currentReview.Product = updateReview.Product;

            _context.Reviews.Update(currentReview);
            _context.SaveChanges();
            return StatusCode(200, currentReview);
        }

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.Find(id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return NoContent();
        }

        // GET/ api/ByProductId/5
        [HttpGet("ByProductId/{productId}")]
        public IActionResult GetbyProductId(int productId)
        {
            var reviews = _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToList();

            return StatusCode(200, reviews);
        }
    }
}
