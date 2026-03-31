# C# 14 Feature: Extension Members - Complete Guide

## ?? Overview

**Extension Members** is a revolutionary new feature in C# 14 that extends the concept of extension methods to include:
- ? Extension **Properties**
- ? **Static** Extension Members
- ? Extension **Operators**

## ?? Table of Contents
1. [What's New](#whats-new)
2. [Syntax](#syntax)
3. [Implementation in POC](#implementation-in-poc)
4. [Testing the Feature](#testing-the-feature)
5. [Benefits](#benefits)
6. [Comparison with C# 13](#comparison)

---

## ?? What's New

### Before C# 14 (Extension Methods Only)
```csharp
public static class ProductExtensions
{
    // Only methods were possible
    public static IEnumerable<Product> WhereInStock(this IEnumerable<Product> products)
    {
        return products.Where(p => p.IsInStock);
    }
    
    // Properties were NOT possible
    // Static extensions were NOT possible
    // Operators were NOT possible
}
```

### C# 14 (Extension Members)
```csharp
extension(IEnumerable<Product> products)
{
    // ? Extension Properties - NEW!
    public bool HasInStockItems => products.Any(p => p.IsInStock);
    public decimal TotalInventoryValue => products.Sum(p => p.Price * p.StockQuantity);
    
    // Extension Methods (improved syntax)
    public IEnumerable<Product> WhereInStock() => products.Where(p => p.IsInStock);
}

extension(IEnumerable<Product>)  // Note: No parameter name for static
{
    // ? Static Extension Properties - NEW!
    public static IEnumerable<Product> Empty => Enumerable.Empty<Product>();
    
    // ? Static Extension Methods - NEW!
    public static IEnumerable<Product> Combine(IEnumerable<Product> first, IEnumerable<Product> second)
        => first.Concat(second);
    
    // ? Extension Operators - NEW!
    public static IEnumerable<Product> operator +(IEnumerable<Product> left, IEnumerable<Product> right)
        => left.Concat(right);
}
```

---

## ?? Syntax

### 1. Extension Block for Instance Members
```csharp
extension(Type parameterName)
{
    // Extension properties
    public PropertyType PropertyName => expression;
    
    // Extension methods
    public ReturnType MethodName(params) { }
}
```

### 2. Extension Block for Static Members
```csharp
extension(Type)  // No parameter name
{
    // Static extension properties
    public static PropertyType PropertyName => expression;
    
    // Static extension methods
    public static ReturnType MethodName(params) { }
    
    // Extension operators
    public static Type operator +(Type left, Type right) { }
}
```

---

## ??? Implementation in POC

### Location
- **Extension Files**: `C#14.NET10POC.Application/Extensions/`
  - `ProductExtensions.cs`
  - `CategoryExtensions.cs`
- **Demo Service**: `C#14.NET10POC.Infrastructure/Services/ExtensionDemoService.cs`
- **Controller**: `C#14.NET10POC/Controllers/ExtensionDemoController.cs`

### Product Extensions (`ProductExtensions.cs`)

#### Instance Extension Properties
```csharp
extension(IEnumerable<Product> products)
{
    public bool HasInStockItems => products.Any(p => p.IsInStock);
    public decimal TotalInventoryValue => products.Sum(p => p.Price * p.StockQuantity);
    public bool IsEmpty => !products.Any();
    public int InStockCount => products.Count(p => p.IsInStock);
}
```

**Usage:**
```csharp
var products = await _context.Products.ToListAsync();

if (products.HasInStockItems)  // Extension Property!
{
    decimal value = products.TotalInventoryValue;  // Extension Property!
    Console.WriteLine($"Total inventory value: ${value}");
}
```

#### Instance Extension Methods
```csharp
extension(IEnumerable<Product> products)
{
    public IEnumerable<Product> WhereInStock() 
        => products.Where(p => p.IsInStock);
    
    public IEnumerable<Product> WherePriceRange(decimal minPrice, decimal maxPrice) 
        => products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
    
    public IEnumerable<Product> WhereLowStock(int threshold = 10) 
        => products.Where(p => p.StockQuantity > 0 && p.StockQuantity <= threshold);
}
```

**Usage:**
```csharp
var inStock = products.WhereInStock();  // Extension Method
var affordable = products.WherePriceRange(10m, 100m);  // Extension Method
var lowStock = products.WhereLowStock(5);  // Extension Method with default parameter
```

#### Static Extension Members
```csharp
extension(IEnumerable<Product>)
{
    // Static Extension Property
    public static IEnumerable<Product> Empty => Enumerable.Empty<Product>();
    
    // Static Extension Method
    public static IEnumerable<Product> Combine(
        IEnumerable<Product> first, 
        IEnumerable<Product> second)
    {
        return first.Concat(second).DistinctBy(p => p.Id);
    }
    
    // Extension Operator
    public static IEnumerable<Product> operator +(
        IEnumerable<Product> left, 
        IEnumerable<Product> right)
    {
        return left.Concat(right);
    }
}
```

**Usage:**
```csharp
// Static Extension Property
var empty = IEnumerable<Product>.Empty;

// Static Extension Method
var combined = IEnumerable<Product>.Combine(products1, products2);

// Extension Operator
var merged = products1 + products2;  // Using + operator!
```

### Category Extensions (`CategoryExtensions.cs`)

Similar structure with category-specific extensions:
- `HasActiveCategories` - Extension property
- `ActiveCount` - Extension property
- `WhereActive()` - Extension method
- `WithProducts()` - Extension method
- `ToCategoryList()` - Extension method

---

## ?? Testing the Feature

### Step 1: Run the Application
```cmd
cd C#14.NET10POC
dotnet run
```

### Step 2: Access Scalar API Documentation
Navigate to: `https://localhost:7xxx/scalar/v1`

### Step 3: Test Extension Demo Endpoints

#### Test 1: Product Extensions
```
GET /api/ExtensionDemo/product-extensions
```

**Expected Response:**
```json
{
  "featureName": "Product Extension Members (Instance)",
  "results": {
    "HasInStockItems (Extension Property)": true,
    "TotalInventoryValue (Extension Property)": 15499.50,
    "IsEmpty (Extension Property)": false,
    "InStockCount (Extension Property)": 2,
    "InStockProducts (Extension Method)": ["Laptop", "C# Programming Book"],
    "PriceRange $20-$100 (Extension Method)": [
      { "name": "C# Programming Book", "price": 49.99 }
    ],
    "LowStockProducts (Extension Method)": [
      { "name": "Laptop", "stockQuantity": 10 }
    ],
    "CalculatedTotalValue (Extension Method)": 15499.50
  },
  "description": "Demonstrates C# 14 extension properties and methods on IEnumerable<Product>"
}
```

#### Test 2: Category Extensions
```
GET /api/ExtensionDemo/category-extensions
```

**Expected Response:**
```json
{
  "featureName": "Category Extension Members (Instance)",
  "results": {
    "HasActiveCategories (Extension Property)": true,
    "ActiveCount (Extension Property)": 3,
    "IsEmpty (Extension Property)": false,
    "ActiveCategories (Extension Method)": ["Electronics", "Books", "Clothing"],
    "CategoriesWithProducts (Extension Method)": [
      { "name": "Electronics", "productCount": 1 },
      { "name": "Books", "productCount": 1 },
      { "name": "Clothing", "productCount": 1 }
    ],
    "CategoryList (Extension Method)": "Electronics, Books, Clothing"
  },
  "description": "Demonstrates C# 14 extension properties and methods on IEnumerable<Category>"
}
```

#### Test 3: Static Extensions
```
GET /api/ExtensionDemo/static-extensions
```

**Expected Response:**
```json
{
  "featureName": "Static Extension Members & Operators",
  "results": {
    "Empty Product Collection (Static Extension Property)": 0,
    "Empty Category Collection (Static Extension Property)": 0,
    "Combined Products (Static Extension Method)": ["Laptop", "C# Programming Book", "T-Shirt"],
    "Sample Collection (Static Extension Method)": [
      { "name": "Sample Product 1", "price": 10.00 },
      { "name": "Sample Product 2", "price": 20.00 },
      { "name": "Sample Product 3", "price": 30.00 }
    ],
    "Products using + operator (Extension Operator)": ["Laptop", "C# Programming Book", "T-Shirt"],
    "Original Products Count": 2,
    "More Products Count": 1,
    "Combined Count": 3
  },
  "description": "Demonstrates C# 14 static extension properties, methods, and custom operators"
}
```

#### Test 4: All Demonstrations
```
GET /api/ExtensionDemo/all
```

Returns all three demonstrations in one response.

---

## ? Benefits

### 1. **More Expressive Code**
```csharp
// Before: Method call
if (GetInStockCount(products) > 0)

// After: Property access (more natural)
if (products.InStockCount > 0)
```

### 2. **Static Extensions**
```csharp
// Create empty collections naturally
var empty = IEnumerable<Product>.Empty;

// Use type-level operations
var combined = IEnumerable<Product>.Combine(list1, list2);
```

### 3. **Custom Operators**
```csharp
// Intuitive collection operations
var all = products1 + products2;  // Concatenate collections
```

### 4. **Better Discoverability**
- IntelliSense shows extension properties alongside methods
- More intuitive API design
- Follows natural language patterns

### 5. **Cleaner Code**
```csharp
// Before
var value = products.Sum(p => p.Price * p.StockQuantity);

// After
var value = products.TotalInventoryValue;  // Clear and concise!
```

---

## ?? Comparison with C# 13

| Feature | C# 13 | C# 14 Extension Members |
|---------|-------|-------------------------|
| Extension Methods | ? | ? |
| Extension Properties | ? | ? |
| Static Extensions | ? | ? |
| Extension Operators | ? | ? |
| Syntax | `public static Return Method(this Type)` | `extension(Type) { }` |
| Cleaner | ? | ? |

---

## ?? Key Takeaways

1. **Extension Properties** - Add computed properties to existing types
2. **Static Extensions** - Extend types at the type level, not just instance level
3. **Extension Operators** - Define custom operators for collections
4. **New Syntax** - `extension(Type)` block is cleaner than `this` parameter
5. **Better APIs** - More natural and expressive code

---

## ?? Real-World Use Cases

### Use Case 1: Collection Utilities
```csharp
if (products.IsEmpty) return;
if (products.HasInStockItems) ProcessInventory();
```

### Use Case 2: Business Logic
```csharp
var totalValue = products.TotalInventoryValue;
var lowStockItems = products.WhereLowStock(10);
```

### Use Case 3: Data Combination
```csharp
var allProducts = localProducts + remoteProducts;
var combined = IEnumerable<Product>.Combine(source1, source2);
```

### Use Case 4: Type-Level Operations
```csharp
var empty = IEnumerable<Product>.Empty;
var samples = IEnumerable<Product>.CreateSampleCollection(5);
```

---

## ?? Next Steps

Try modifying the extensions:
1. Add your own extension properties
2. Create custom operators
3. Implement domain-specific extensions
4. Combine with other C# 14 features

---

**Implemented in**: C#14.NET10POC  
**Feature Status**: ? Fully Implemented  
**API Endpoints**: `/api/ExtensionDemo/*`  
**Documentation**: This file
