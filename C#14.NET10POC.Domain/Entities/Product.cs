namespace C_14.NET10POC.Domain.Entities;

/// <summary>
/// Product entity demonstrating C# 14 field-backed properties
/// </summary>
public class Product
{
    public int Id { get; set; }

    // C# 14 Feature: Field-backed property with validation using 'field' keyword
    public string Name
    {
        get => field;
        set => field = !string.IsNullOrWhiteSpace(value) 
            ? value.Trim() 
            : throw new ArgumentException("Product name cannot be empty", nameof(value));
    }

    // C# 14 Feature: Field-backed property with null check
    public string Description
    {
        get => field ?? string.Empty;
        set => field = value?.Trim();
    }

    public decimal Price { get; set; }
    
    public int StockQuantity { get; set; }
    
    public int CategoryId { get; set; }
    
    // Navigation property
    public Category? Category { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;

    // Computed property
    public bool IsInStock => StockQuantity > 0;
}
