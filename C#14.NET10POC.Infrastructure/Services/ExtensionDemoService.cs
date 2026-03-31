using C_14.NET10POC.Application.Extensions;
using C_14.NET10POC.Application.Services;
using C_14.NET10POC.Domain.Entities;
using C_14.NET10POC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace C_14.NET10POC.Infrastructure.Services;

/// <summary>
/// Implementation demonstrating C# 14 Extension Members
/// </summary>
public class ExtensionDemoService : IExtensionDemoService
{
    private readonly ApplicationDbContext _context;

    public ExtensionDemoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ExtensionDemoResult> DemonstrateProductExtensions()
    {
        var products = await _context.Products.ToListAsync();

        var results = new Dictionary<string, object>
        {
            // C# 14 Feature: Extension Properties
            ["HasInStockItems (Extension Property)"] = products.HasInStockItems,
            ["TotalInventoryValue (Extension Property)"] = products.TotalInventoryValue,
            ["IsEmpty (Extension Property)"] = products.IsEmpty,
            ["InStockCount (Extension Property)"] = products.InStockCount,

            // C# 14 Feature: Extension Methods
            ["InStockProducts (Extension Method)"] = products.WhereInStock().Select(p => p.Name).ToList(),
            ["PriceRange $20-$100 (Extension Method)"] = products.WherePriceRange(20m, 100m).Select(p => new { p.Name, p.Price }).ToList(),
            ["LowStockProducts (Extension Method)"] = products.WhereLowStock(15).Select(p => new { p.Name, p.StockQuantity }).ToList(),
            ["CalculatedTotalValue (Extension Method)"] = products.CalculateTotalValue()
        };

        return new ExtensionDemoResult(
            "Product Extension Members (Instance)",
            results,
            "Demonstrates C# 14 extension properties and methods on IEnumerable<Product>"
        );
    }

    public async Task<ExtensionDemoResult> DemonstrateCategoryExtensions()
    {
        var categories = await _context.Categories.Include(c => c.Products).ToListAsync();

        var results = new Dictionary<string, object>
        {
            // C# 14 Feature: Extension Properties
            ["HasActiveCategories (Extension Property)"] = categories.HasActiveCategories,
            ["ActiveCount (Extension Property)"] = categories.ActiveCount,
            ["IsEmpty (Extension Property)"] = categories.IsEmpty,

            // C# 14 Feature: Extension Methods
            ["ActiveCategories (Extension Method)"] = categories.WhereActive().Select(c => c.Name).ToList(),
            ["CategoriesWithProducts (Extension Method)"] = categories.WithProducts().Select(c => new { c.Name, ProductCount = c.Products.Count }).ToList(),
            ["CategoryList (Extension Method)"] = categories.ToCategoryList()
        };

        return new ExtensionDemoResult(
            "Category Extension Members (Instance)",
            results,
            "Demonstrates C# 14 extension properties and methods on IEnumerable<Category>"
        );
    }

    public async Task<ExtensionDemoResult> DemonstrateStaticExtensions()
    {
        var products = await _context.Products.Take(2).ToListAsync();
        var moreProducts = await _context.Products.Skip(2).Take(1).ToListAsync();

        var results = new Dictionary<string, object>
        {
            // C# 14 Feature: Static Extension Properties
            ["Empty Product Collection (Static Extension Property)"] = IEnumerable<Product>.Empty.Count(),
            ["Empty Category Collection (Static Extension Property)"] = IEnumerable<Category>.Empty.Count(),

            // C# 14 Feature: Static Extension Methods
            ["Combined Products (Static Extension Method)"] = IEnumerable<Product>.Combine(products, moreProducts).Select(p => p.Name).ToList(),
            ["Sample Collection (Static Extension Method)"] = IEnumerable<Product>.CreateSampleCollection(3).Select(p => new { p.Name, p.Price }).ToList(),

            // C# 14 Feature: Extension Operators
            ["Products using + operator (Extension Operator)"] = (products + moreProducts).Select(p => p.Name).ToList(),
            
            ["Original Products Count"] = products.Count(),
            ["More Products Count"] = moreProducts.Count(),
            ["Combined Count"] = (products + moreProducts).Count()
        };

        return new ExtensionDemoResult(
            "Static Extension Members & Operators",
            results,
            "Demonstrates C# 14 static extension properties, methods, and custom operators"
        );
    }
}
