namespace C_14.NET10POC.Application.Models;

/// <summary>
/// Testing C# 14 partial properties feature
/// </summary>
public partial class PartialPropertyExample
{
    // C# 14 Feature: Partial property declaration
    public partial string Name { get; set; }
    
    public partial int Age { get; set; }
}

// Second part of the partial class
public partial class PartialPropertyExample
{
    // C# 14 Feature: Implementing the partial property
    public partial string Name 
    { 
        get => _name ?? "Unknown";
        set => _name = value?.Trim();
    }
    
    private string? _name;
    
    public partial int Age
    {
        get => _age;
        set => _age = value >= 0 ? value : throw new ArgumentException("Age cannot be negative");
    }
    
    private int _age;
}
