using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Tracking.Models;

namespace Tracking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId);

                entity.Property(p => p.Name)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(p => p.Price)
                      .HasPrecision(18, 2);

                // Index for searching products by name
                entity.HasIndex(p => p.Name);

                // Soft Delete global filter
                entity.HasQueryFilter(p => !p.IsDeleted);

                // SQL Server concurrency token
                entity.Property(p => p.RowVersion)
                      .IsRowVersion();
            });
        }
    }
}
