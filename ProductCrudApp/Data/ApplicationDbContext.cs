using Microsoft.EntityFrameworkCore;
using ProductCrudApp.Model;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProductCrudApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Cascade Delete
            modelBuilder.Entity<Product>()
               .HasOne<Category>()  // Product belongs to one Category
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents category deletion if products exist
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
