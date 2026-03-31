using C_14.NET10POC.Application.DTOs;
using C_14.NET10POC.Application.Interfaces;
using C_14.NET10POC.Domain.Entities;
using C_14.NET10POC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace C_14.NET10POC.Infrastructure.Services;

/// <summary>
/// Product service demonstrating C# 14 features including null-conditional assignment
/// </summary>
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();

        return products.Select(MapToDto);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        return product is not null ? MapToDto(product) : null;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Reload with category
        await _context.Entry(product).Reference(p => p.Category).LoadAsync();

        return MapToDto(product);
    }

    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto dto)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return null;

        // C# 14 Feature: Null-conditional assignment
        // Only update if the property is provided (not null)
        product?.Name = dto.Name ?? product.Name;
        product?.Description = dto.Description ?? product.Description;
        
        if (dto.Price.HasValue)
            product!.Price = dto.Price.Value;
        
        if (dto.StockQuantity.HasValue)
            product!.StockQuantity = dto.StockQuantity.Value;
        
        if (dto.CategoryId.HasValue)
            product!.CategoryId = dto.CategoryId.Value;
        
        if (dto.IsActive.HasValue)
            product!.IsActive = dto.IsActive.Value;

        product!.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        
        if (product is null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

        return products.Select(MapToDto);
    }

    public async Task<IEnumerable<ProductDto>> GetInStockProductsAsync()
    {
        // Query products with stock quantity > 0
        // Using standard EF Core filtering
        var products = await _context.Products
            .Where(p => p.StockQuantity > 0)
            .Include(p => p.Category)
            .ToListAsync();

        return products.Select(MapToDto);
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.CategoryId,
            product.Category?.Name ?? "Unknown",
            product.IsActive,
            product.IsInStock,
            product.CreatedAt,
            product.UpdatedAt
        );
    }
}
