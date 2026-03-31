# ?? Quick Test Guide - C# 14 Extension Members

## Run the Application

```cmd
cd C#14.NET10POC
dotnet run
```

## Access Scalar API Documentation

Navigate to: `https://localhost:7xxx/scalar/v1`

(Replace `7xxx` with your actual port number shown in console)

---

## ?? Test Extension Members

### 1. Product Extension Properties & Methods

**Endpoint**: `GET /api/ExtensionDemo/product-extensions`

**What it demonstrates**:
- ? `HasInStockItems` - Extension Property
- ? `TotalInventoryValue` - Extension Property  
- ? `IsEmpty` - Extension Property
- ? `InStockCount` - Extension Property
- ? `WhereInStock()` - Extension Method
- ? `WherePriceRange()` - Extension Method
- ? `WhereLowStock()` - Extension Method

**Try it in Scalar**:
1. Find "Extension Demo" section
2. Click on "product-extensions"
3. Click "Try it out"
4. Click "Execute"

---

### 2. Category Extension Properties & Methods

**Endpoint**: `GET /api/ExtensionDemo/category-extensions`

**What it demonstrates**:
- ? `HasActiveCategories` - Extension Property
- ? `ActiveCount` - Extension Property
- ? `WhereActive()` - Extension Method
- ? `WithProducts()` - Extension Method
- ? `ToCategoryList()` - Extension Method

---

### 3. Static Extensions & Operators

**Endpoint**: `GET /api/ExtensionDemo/static-extensions`

**What it demonstrates**:
- ? `IEnumerable<Product>.Empty` - Static Extension Property
- ? `IEnumerable<Product>.Combine()` - Static Extension Method
- ? `products1 + products2` - Extension Operator (!)
- ? `IEnumerable<Product>.CreateSampleCollection()` - Static Extension Method

---

### 4. All Demonstrations

**Endpoint**: `GET /api/ExtensionDemo/all`

Returns all three demonstrations in one response.

---

## ?? What Makes This Special?

### Before C# 14:
```csharp
// Only extension methods were possible
products.Where(p => p.IsInStock)
products.Sum(p => p.Price * p.StockQuantity)
```

### With C# 14 Extension Members:
```csharp
// Extension PROPERTIES - much cleaner!
products.HasInStockItems        // ? Property, not method!
products.TotalInventoryValue    // ? Property, not method!
products.InStockCount          // ? Property, not method!

// Static Extensions
IEnumerable<Product>.Empty     // ? Static extension property!
IEnumerable<Product>.Combine(a, b)  // ? Static extension method!

// Extension Operators
var all = products1 + products2  // ? Custom + operator!
```

---

## ?? Expected Results

When you call `/api/ExtensionDemo/product-extensions`, you should see:

```json
{
  "featureName": "Product Extension Members (Instance)",
  "results": {
    "HasInStockItems (Extension Property)": true,
    "TotalInventoryValue (Extension Property)": 15499.50,
    "IsEmpty (Extension Property)": false,
    "InStockCount (Extension Property)": 2,
    "InStockProducts (Extension Method)": [
      "Laptop",
      "C# Programming Book"
    ],
    "PriceRange $20-$100 (Extension Method)": [
      {
        "name": "C# Programming Book",
        "price": 49.99
      }
    ],
    "LowStockProducts (Extension Method)": [
      {
        "name": "Laptop",
        "stockQuantity": 10
      }
    ],
    "CalculatedTotalValue (Extension Method)": 15499.50
  },
  "description": "Demonstrates C# 14 extension properties and methods on IEnumerable<Product>"
}
```

---

## ? Checklist

- [ ] Application is running
- [ ] Accessed Scalar at `/scalar/v1`
- [ ] Tested product extensions endpoint
- [ ] Tested category extensions endpoint
- [ ] Tested static extensions endpoint
- [ ] Reviewed the returned data
- [ ] Understood extension properties vs methods
- [ ] Understood static extensions
- [ ] Understood extension operators

---

## ?? Key Learnings

1. **Extension Properties** - You can now add properties (not just methods) to existing types
2. **Static Extensions** - You can extend types at the type level, not just instance level
3. **Extension Operators** - You can define custom operators like `+` for collections
4. **Cleaner Syntax** - The `extension(Type)` block is cleaner than `this Type` parameter
5. **Better APIs** - More natural and expressive code

---

## ?? More Information

See `docs/Feature3_ExtensionMembers_Guide.md` for complete documentation.

---

**Feature Status**: ? Fully Implemented  
**C# Version**: 14.0  
**.NET Version**: 10.0
