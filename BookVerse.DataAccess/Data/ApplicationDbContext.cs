using BookVerse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.DataAccess.Data
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //data seeding for category table
            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Action", Description = "Actions are coming Soon", DisplayOrder = 1 },
                    new Category { Id = 2, Name = "Mystery", Description = "Upcoming Mystery", DisplayOrder = 2 },
                    new Category { Id = 3, Name = "SciFi", Description = "Science Fiction", DisplayOrder = 3 }
                );

            //data seeding for product table
            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id=1,
                        Title= "THE GUIDE",
                        Description= "The novel describes the transformation of the protagonist, Raju, from a tour guide to a spiritual guide and then one of the greatest holy men of India.",
                        Author= "R. K. Narayan",
                        ISBN="10001",
                        ListPrice=200,
                        CategoryId = 1,
                        Image = ""
                    },
                    new Product
                    {
                        Id = 2,
                        Title = "The India Story",
                        Description = "The novel describes the transformation of the protagonist, Raju, from a tour guide to a spiritual guide and then one of the greatest holy men of India.",
                        Author = "Bimal Jalal",
                        ISBN = "10002",
                        ListPrice = 300,
                        CategoryId = 1,
                        Image = ""
                    }
                );;
        }
    }
}
