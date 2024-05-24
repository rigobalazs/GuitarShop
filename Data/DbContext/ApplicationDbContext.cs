using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Guitar> Guitars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Guitar>().HasData(
                new Guitar {
                    Id = 1,
                    Name = "Matt Heafy Les Paul Custom 6-string",
                    Description = "A Highly Versatile Guitar That’s Made for Metal",
                    Price = 1199,
                    Category = "Electric guitar",
                }, new Guitar
                {
                    Id = 2,
                    Name = "Matt Heafy Les Paul Custom 7-string",
                    Description = "A Highly Versatile Guitar That’s Made for Metal",
                    Price = 1399,
                    Category = "Electric guitar",
                }, new Guitar
                {
                    Id = 3,
                    Name = "Fender Stratocaster",
                    Description = "Please fill it",
                    Price = 1500,
                    Category = "Electric guitar",
                },
                new Guitar
                {
                    Id = 4,
                    Name = "Stagg accoustic sample",
                    Description = "Please fill it",
                    Price = 200,
                    Category = "Accoustic guitar",
                },
                new Guitar
                {
                    Id = 5,
                    Name = "Bass guitar",
                    Description = "Please fill it",
                    Price = 100,
                    Category = "Bass guitar",
                }
            );
        }
    }
}
