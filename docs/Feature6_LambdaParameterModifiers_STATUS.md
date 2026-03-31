# C# 14 Feature: Lambda Parameter Modifiers - Status Report

## ?? Feature Status: Not Yet Available in .NET 10 Preview

**Lambda Parameter Modifiers** is a planned C# 14 feature that is **not yet available** in the current .NET 10 preview build. This documentation explains what the feature will enable once it's available.

---

## ?? What This Feature Will Enable

Lambda parameter modifiers will allow you to use `ref`, `in`, `out`, and `params` modifiers on lambda expression parameters, enabling:

1. **ref parameters** - In-place modification without copying
2. **in parameters** - Read-only by-reference access
3. **out parameters** - Multiple return values
4. **params parameters** - Variable-length arguments

---

## ?? Planned Syntax (Once Available)

### ref Parameters
```csharp
// Will enable in-place modifications
int[] prices = { 100, 200, 300 };
Array.ForEach(prices, (ref int price) => price *= 1.1m);
```

### in Parameters
```csharp
// Will enable zero-copy read-only access
Func<Product, bool> isExpensive = (in Product p) => p.Price > 100m;
```

### out Parameters  
```csharp
// Will enable multiple return values
delegate bool TryParse(string input, out decimal result);
TryParse parse = (string input, out decimal result) => 
    decimal.TryParse(input, out result);
```

### params Parameters
```csharp
// Will enable variable-length arguments
delegate decimal Sum(params decimal[] values);
Sum sum = (params decimal[] values) => values.Sum();
```

---

## ?? Current Status

### What Works Today (C# 13 / .NET 9)
- Lambda expressions with regular parameters
- Method parameters with modifiers (`ref`, `in`, `out`, `params`)
- Local functions with modifiers

### What Doesn't Work Yet (.NET 10 Preview)
- ? `ref` modifiers on lambda parameters
- ? `in` modifiers on lambda parameters  
- ? `out` modifiers on lambda parameters
- ? `params` modifiers on lambda parameters

---

## ?? Why This Feature Matters

### 1. Performance
```csharp
// Today: Copies struct
Func<Product, bool> check = (Product p) => p.Price > 100m;

// Future: Zero-copy with 'in'
Func<Product, bool> check = (in Product p) => p.Price > 100m; ?
```

### 2. In-Place Modifications
```csharp
// Today: Need explicit array indexing
for (int i = 0; i < array.Length; i++)
    array[i] *= 2;

// Future: Cleaner with 'ref'
Array.ForEach(array, (ref int x) => x *= 2); ?
```

### 3. Multiple Return Values
```csharp
// Today: Need tuple or separate variables
(bool success, decimal value) = ParsePrice(input);

// Future: Natural 'out' parameters
TryParsePrice(input, out decimal value); ?
```

---

## ?? Workarounds Until Feature is Available

### Instead of ref in Lambda
```csharp
// Use for loop
int[] prices = { 100, 200, 300 };
for (int i = 0; i < prices.Length; i++)
{
    prices[i] = (int)(prices[i] * 1.1m);
}
```

### Instead of in in Lambda
```csharp
// Use regular parameters (will copy)
Func<Product, bool> isExpensive = (Product p) => p.Price > 100m;
```

### Instead of out in Lambda
```csharp
// Use tuples
Func<string, (bool success, decimal value)> tryParse = 
    (string input) => 
    {
        bool success = decimal.TryParse(input, out decimal value);
        return (success, value);
    };
```

### Instead of params in Lambda
```csharp
// Use array parameter
Func<decimal[], decimal> sum = (decimal[] values) => values.Sum();
```

---

## ?? When Will This Be Available?

This feature is planned for C# 14 but is not yet implemented in the current .NET 10 preview. It may become available in:
- Future .NET 10 preview releases
- .NET 10 RC (Release Candidate)
- .NET 10 RTM (final release)

Check the official C# 14 release notes for updates.

---

## ?? References

- [C# 14 Feature Proposals](https://github.com/dotnet/csharplang/issues)
- [.NET 10 Preview Release Notes](https://github.com/dotnet/core/tree/main/release-notes/10.0)
- [Lambda Expression Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions)

---

## ? What's Implemented in This POC

Since Lambda Parameter Modifiers are not yet available, this POC focuses on the 5 features that ARE working:

1. ? **Field-backed Properties** (`field` keyword)
2. ? **Null-conditional Assignment**
3. ? **Extension Members** (Properties, Static, Operators)
4. ? **`nameof` with Unbound Generic Types**
5. ? **Implicit Span Conversions**

---

## ?? Note for Developers

If you're reading this and Lambda Parameter Modifiers are now available in your .NET version:

1. Check the C# language version in your project file
2. Update this POC to include working examples
3. Remove this notice
4. Implement the demos as originally planned

---

**Status**: ? Awaiting Implementation in .NET 10  
**Last Checked**: January 7, 2026  
**Alternative**: Use workarounds documented above
