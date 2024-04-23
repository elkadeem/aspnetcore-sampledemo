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

        public DbSet<Deparment> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(builder =>
            {
                builder.Property(b => b.Email).HasMaxLength(100);
                builder.Property(b => b.Phone).HasMaxLength(20);
                builder.Property(b => b.IdPhotoContentType).HasMaxLength(70);
            });

            modelBuilder.Entity<Employee>(builder => { 
               builder.OwnsOne(b => b.Name, name =>
               {
                   name.Property(n => n.FirstName).HasColumnName("FirstName");
                   name.Property(n => n.FatherName).HasColumnName("FatherName");
                   name.Property(n => n.MiddleName).HasColumnName("MiddleName");
                   name.Property(n => n.LastName).HasColumnName("LastName");
               });

               
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
