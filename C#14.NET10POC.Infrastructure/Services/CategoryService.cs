using C_14.NET10POC.Application.DTOs;
using C_14.NET10POC.Application.Interfaces;
using C_14.NET10POC.Domain.Entities;
using C_14.NET10POC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace C_14.NET10POC.Infrastructure.Services;

/// <summary>
/// Category service demonstrating C# 14 null-conditional assignment
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .ToListAsync();

        return categories.Select(MapToDto);
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        return category is not null ? MapToDto(category) : null;
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return MapToDto(category);
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
            return null;

        // C# 14 Feature: Null-conditional assignment
        category?.Name = dto.Name ?? category.Name;
        category?.Description = dto.Description ?? category.Description;
        
        if (dto.IsActive.HasValue)
            category!.IsActive = dto.IsActive.Value;

        category!.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToDto(category);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        
        if (category is null)
            return false;

        // Check if category has products
        var hasProducts = await _context.Products.AnyAsync(p => p.CategoryId == id);
        if (hasProducts)
            throw new InvalidOperationException("Cannot delete category with existing products");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        
        return true;
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.Products?.Count ?? 0,
            category.CreatedAt,
            category.UpdatedAt
        );
    }
}
