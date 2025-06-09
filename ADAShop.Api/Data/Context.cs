using ADAShop.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ADAShop.Api.Data
{
    public class Context : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasIndex(appUser => appUser.Email)
                .IsUnique();
            builder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
        }
    }
}