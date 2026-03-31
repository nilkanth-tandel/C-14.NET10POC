namespace C_14.NET10POC.Domain.Entities;

/// <summary>
/// Category entity demonstrating C# 14 field-backed properties
/// </summary>
public class Category
{
    public int Id { get; set; }

    // C# 14 Feature: Field-backed property with validation
    public string Name
    {
        get => field;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Category name cannot be empty", nameof(value));
    }

    // C# 14 Feature: Field-backed property with default value handling
    public string Description
    {
        get => field ?? string.Empty;
        set => field = value?.Trim();
    }

    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
