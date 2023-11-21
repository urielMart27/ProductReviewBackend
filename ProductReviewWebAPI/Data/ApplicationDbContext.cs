using Microsoft.EntityFrameworkCore;
using ProductReviewWebAPI.Models;

namespace ProductReviewWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products{ get; set; }
        public  DbSet<Review> Reviews { get; set; }
        public ApplicationDbContext(DbContextOptions 
            options) : base(options)
        {

        }
    }
}
