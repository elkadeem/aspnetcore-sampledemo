using Microsoft.EntityFrameworkCore;

namespace ApplicationSample.Web.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
