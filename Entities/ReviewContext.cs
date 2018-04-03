using Microsoft.EntityFrameworkCore;
using System;
namespace CustomerServiceAPI.Entities
{
    public class ReviewContext : DbContext
    {
        public ReviewContext(DbContextOptions<ReviewContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Review> Reviews { get; set; }
    }
}
