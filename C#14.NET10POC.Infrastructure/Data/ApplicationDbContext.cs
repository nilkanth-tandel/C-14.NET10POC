using C_14.NET10POC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace C_14.NET10POC.Infrastructure.Data;

/// <summary>
/// DbContext for EF Core 10 with Product Catalog
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Product entity
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
            
            entity.Property(e => e.Price)
                .HasPrecision(18, 2);

            // Global query filter for active products
            entity.HasQueryFilter(p => p.IsActive);

            // Relationship
            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index for performance
            entity.HasIndex(p => p.CategoryId);
            entity.HasIndex(p => p.IsActive);
        });

        // Configure Category entity
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            // Global query filter for active categories
            entity.HasQueryFilter(c => c.IsActive);

            // Index
            entity.HasIndex(c => c.Name).IsUnique();
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Use static datetime to avoid model changes on every build
        var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Electronics",
                Description = "Electronic devices and accessories",
                IsActive = true,
                CreatedAt = seedDate
            },
            new Category
            {
                Id = 2,
                Name = "Books",
                Description = "Physical and digital books",
                IsActive = true,
                CreatedAt = seedDate
            },
            new Category
            {
                Id = 3,
                Name = "Clothing",
                Description = "Fashion and apparel",
                IsActive = true,
                CreatedAt = seedDate
            }
        );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "High-performance laptop",
                Price = 1299.99m,
                StockQuantity = 10,
                CategoryId = 1,
                IsActive = true,
                CreatedAt = seedDate
            },
            new Product
            {
                Id = 2,
                Name = "C# Programming Book",
                Description = "Comprehensive guide to C# 14",
                Price = 49.99m,
                StockQuantity = 50,
                CategoryId = 2,
                IsActive = true,
                CreatedAt = seedDate
            },
            new Product
            {
                Id = 3,
                Name = "T-Shirt",
                Description = "Cotton t-shirt",
                Price = 19.99m,
                StockQuantity = 0,
                CategoryId = 3,
                IsActive = true,
                CreatedAt = seedDate
            }
        );
    }
}
