using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ApplicationSample.Web.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(builder =>
            {
                builder.Property(b => b.Email).HasMaxLength(100);
                builder.Property(b => b.Phone).HasMaxLength(20);
                builder.Property(b => b.IdPhotoContentType).HasMaxLength(70);
            });

            //modelBuilder.Entity<Customer>()
            //    .Property(b => b.Email)
            //    .HasMaxLength(100);

            //modelBuilder.Entity<Customer>()
            //    .Property(b => b.Phone)
            //    .HasMaxLength(20);
        }
    }
}
