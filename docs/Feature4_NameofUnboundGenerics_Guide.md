# C# 14 Feature: `nameof` with Unbound Generic Types - Complete Guide

## ?? Overview

C# 14 introduces the ability to use `nameof` with **unbound generic types** - generic types without type arguments specified (e.g., `List<>`, `Dictionary<,>`). This provides clean, readable type names for logging, error messages, and documentation.

## ?? Table of Contents
1. [What's New](#whats-new)
2. [Syntax](#syntax)
3. [Benefits](#benefits)
4. [Implementation in POC](#implementation-in-poc)
5. [Testing the Feature](#testing-the-feature)
6. [Comparison with C# 13](#comparison)
7. [Real-World Use Cases](#real-world-use-cases)

---

## ?? What's New

### Before C# 14
```csharp
// ? Not possible - compiler error
// var name = nameof(List<>);

// Had to use typeof().Name - but it includes backticks and arity
var name = typeof(List<>).Name;  // Returns "List`1" ??
var name2 = typeof(Dictionary<,>).Name;  // Returns "Dictionary`2" ??
```

### C# 14
```csharp
// ? Clean type names without type arguments!
var name = nameof(List<>);           // Returns "List" ?
var name2 = nameof(Dictionary<,>);   // Returns "Dictionary" ?
var name3 = nameof(IEnumerable<>);   // Returns "IEnumerable" ?
```

---

## ?? Syntax

### Single Type Parameter
```csharp
nameof(List<>)          // "List"
nameof(IEnumerable<>)   // "IEnumerable"
nameof(Task<>)          // "Task"
nameof(Nullable<>)      // "Nullable"
```

### Multiple Type Parameters
```csharp
nameof(Dictionary<,>)   // "Dictionary"
nameof(Func<,>)         // "Func"
nameof(Action<,>)       // "Action"
```

### Custom Generic Types
```csharp
public class Repository<T> { }

var name = nameof(Repository<>);  // "Repository"
```

---

## ? Benefits

### 1. **Cleaner Logging**
```csharp
// Before C# 14
_logger.LogInformation($"Initializing {typeof(List<>).Name}");
// Output: "Initializing List`1" ??

// C# 14
_logger.LogInformation($"Initializing {nameof(List<>)}");
// Output: "Initializing List" ?
```

### 2. **Better Error Messages**
```csharp
// Before
throw new InvalidOperationException($"Cannot convert {typeof(List<>).Name} to array");
// Error: "Cannot convert List`1 to array" ??

// C# 14
throw new InvalidOperationException($"Cannot convert {nameof(List<>)} to array");
// Error: "Cannot convert List to array" ?
```

### 3. **Readable Documentation**
```csharp
// Before
var doc = $"See documentation for {typeof(Dictionary<,>).Name} usage";
// "See documentation for Dictionary`2 usage" ??

// C# 14
var doc = $"See documentation for {nameof(Dictionary<,>)} usage";
// "See documentation for Dictionary usage" ?
```

### 4. **Type Registry**
```csharp
var typeRegistry = new Dictionary<string, string>
{
    [nameof(List<>)] = "Mutable collection",
    [nameof(IEnumerable<>)] = "Read-only sequence",
    [nameof(Dictionary<,>)] = "Key-value pairs"
};
```

---

## ??? Implementation in POC

### Location
- **Service Interface**: `C#14.NET10POC.Application/Services/INameofDemoService.cs`
- **Service Implementation**: `C#14.NET10POC.Infrastructure/Services/NameofDemoService.cs`
- **Controller**: `C#14.NET10POC/Controllers/NameofDemoController.cs`

### Examples Implemented

#### 1. Unbound Generic Types
```csharp
public NameofDemoResult DemonstrateUnboundGenerics()
{
    var examples = new Dictionary<string, string>
    {
        ["List<> (unbound)"] = nameof(List<>),              // "List"
        ["Dictionary<,> (unbound)"] = nameof(Dictionary<,>), // "Dictionary"
        ["IEnumerable<> (unbound)"] = nameof(IEnumerable<>), // "IEnumerable"
        ["Task<> (unbound)"] = nameof(Task<>),              // "Task"
        
        // Comparison
        ["typeof(List<>).Name"] = typeof(List<>).Name,      // "List`1" (old way)
        ["nameof(List<>)"] = nameof(List<>)                 // "List" (C# 14)
    };
    
    return new NameofDemoResult(...);
}
```

#### 2. Logging Scenarios
```csharp
public NameofDemoResult DemonstrateLoggingScenarios()
{
    // Repository logging
    var log1 = $"Initializing {nameof(IEnumerable<>)} repository for {nameof(Product)}";
    
    // Cache key generation
    var cacheKey = $"Cache_{nameof(List<>)}_{nameof(Product)}_InStock";
    
    // Collection conversion
    var log2 = $"Converting {nameof(IEnumerable<>)} to {nameof(List<>)}";
    
    // Actual logging with ILogger
    _logger.LogInformation("Using {CollectionType} for {EntityType}",
        nameof(IEnumerable<>),
        nameof(Product));
    
    return new NameofDemoResult(...);
}
```

#### 3. Error Messages
```csharp
public NameofDemoResult DemonstrateErrorMessages()
{
    // Validation error
    var error1 = $"Expected {nameof(IEnumerable<>)} but received null";
    
    // Type mismatch
    var error2 = $"Cannot convert {nameof(List<>)} to {nameof(Dictionary<,>)}";
    
    // Serialization error
    var error3 = $"Failed to serialize {nameof(Dictionary<,>)} containing {nameof(Product)}";
    
    return new NameofDemoResult(...);
}
```

#### 4. Type Comparison
```csharp
public NameofDemoResult DemonstrateTypeComparison()
{
    var examples = new Dictionary<string, string>
    {
        // Show the difference
        ["typeof(List<>).Name"] = typeof(List<>).Name,       // "List`1"
        ["nameof(List<>)"] = nameof(List<>),                 // "List"
        ["typeof(Dictionary<,>).Name"] = typeof(Dictionary<,>).Name, // "Dictionary`2"
        ["nameof(Dictionary<,>)"] = nameof(Dictionary<,>)    // "Dictionary"
    };
    
    return new NameofDemoResult(...);
}
```

---

## ?? Testing the Feature

### Step 1: Run the Application
```cmd
cd C#14.NET10POC
dotnet run
```

### Step 2: Access Scalar API Documentation
Navigate to: `https://localhost:7xxx/scalar/v1`

### Step 3: Test Nameof Demo Endpoints

#### Test 1: Unbound Generics
```
GET /api/NameofDemo/unbound-generics
```

**Expected Response:**
```json
{
  "featureName": "nameof with Unbound Generic Types",
  "examples": {
    "List<> (unbound)": "List",
    "Dictionary<,> (unbound)": "Dictionary",
    "IEnumerable<> (unbound)": "IEnumerable",
    "ICollection<> (unbound)": "ICollection",
    "HashSet<> (unbound)": "HashSet",
    "Queue<> (unbound)": "Queue",
    "Stack<> (unbound)": "Stack",
    "Nullable<> (unbound)": "Nullable",
    "Task<> (unbound)": "Task",
    "ValueTask<> (unbound)": "ValueTask",
    "Func<> (unbound)": "Func",
    "Action<> (unbound)": "Action",
    "typeof(List<>).Name (old way)": "List`1",
    "nameof(List<>) (C# 14)": "List"
  },
  "description": "Demonstrates C# 14 ability to use nameof with unbound generic types"
}
```

#### Test 2: Logging Scenarios
```
GET /api/NameofDemo/logging-scenarios
```

**Expected Response:**
```json
{
  "featureName": "nameof in Logging Scenarios",
  "examples": {
    "Repository initialization": "Initializing IEnumerable repository for Product",
    "Cache key": "Cache_List_Product_InStock",
    "Collection conversion": "Converting IEnumerable to List",
    "Generic method call": "Calling generic method with Dictionary parameter",
    "Factory creation": "Creating instance of Task for Product query",
    "Old way with typeof": "Working with List`1",
    "New way with nameof": "Working with List"
  },
  "description": "Shows practical logging scenarios using nameof with unbound generics"
}
```

#### Test 3: Error Messages
```
GET /api/NameofDemo/error-messages
```

**Expected Response:**
```json
{
  "featureName": "nameof in Error Messages",
  "examples": {
    "Validation error": "Expected IEnumerable but received null for Product collection",
    "Type mismatch": "Cannot convert List to Dictionary",
    "Null reference": "Null reference exception in Task returning Product",
    "Configuration error": "Missing configuration for ICollection of type Category",
    "Serialization error": "Failed to serialize Dictionary containing Product entities",
    "Constraint error": "Type constraint violation: IEnumerable must be reference type",
    "Old error (with typeof)": "Error in List`1 operation",
    "New error (with nameof)": "Error in List operation"
  },
  "description": "Demonstrates using nameof with unbound generics for clearer error messages"
}
```

#### Test 4: Type Comparison
```
GET /api/NameofDemo/type-comparison
```

Shows side-by-side comparison of `typeof().Name` vs `nameof()`.

#### Test 5: All Demonstrations
```
GET /api/NameofDemo/all
```

Returns all demonstrations in one response.

#### Test 6: Live Logging Example
```
POST /api/NameofDemo/log-example
Content-Type: application/json

{
  "customMessage": "Testing nameof feature"
}
```

This actually logs a message using C# 14 nameof - check your application logs!

---

## ?? Comparison with C# 13

| Aspect | C# 13 | C# 14 |
|--------|-------|-------|
| `nameof(List<>)` | ? Compiler error | ? Returns "List" |
| `nameof(Dictionary<,>)` | ? Compiler error | ? Returns "Dictionary" |
| Alternative | `typeof(List<>).Name` ? "List`1" | `nameof(List<>)` ? "List" |
| Readability | ? Backticks and arity | ? Clean names |
| Logging | ? Ugly output | ? Professional output |
| Error Messages | ? Technical looking | ? User-friendly |

---

## ?? Real-World Use Cases

### Use Case 1: Repository Pattern
```csharp
public class Repository<T> where T : class
{
    private readonly ILogger _logger;
    
    public Repository(ILogger logger)
    {
        _logger = logger;
        
        // C# 14: Clean logging
        _logger.LogInformation("Initialized {RepositoryType} for {EntityType}",
            nameof(Repository<>),
            typeof(T).Name);
    }
}
```

### Use Case 2: Cache Key Generation
```csharp
public string GenerateCacheKey<T>(string operation)
{
    // C# 14: Clean cache keys
    return $"{nameof(List<>)}_{typeof(T).Name}_{operation}";
    // Example: "List_Product_GetAll"
}
```

### Use Case 3: Exception Messages
```csharp
public void ValidateCollection<T>(IEnumerable<T> collection)
{
    if (collection == null)
    {
        // C# 14: Clean error message
        throw new ArgumentNullException(
            nameof(collection),
            $"Expected {nameof(IEnumerable<>)} of {typeof(T).Name} but received null");
    }
}
```

### Use Case 4: API Documentation
```csharp
/// <summary>
/// Converts {nameof(IEnumerable<>)} to {nameof(List<>)}
/// </summary>
public List<T> ToList<T>(IEnumerable<T> source)
{
    return source.ToList();
}
```

### Use Case 5: Diagnostic Messages
```csharp
public void PerformanceMonitor<T>(Func<T> operation)
{
    var stopwatch = Stopwatch.StartNew();
    var result = operation();
    stopwatch.Stop();
    
    // C# 14: Clean performance logging
    _logger.LogInformation(
        "Operation {OperationType} completed in {ElapsedMs}ms",
        nameof(Func<>),
        stopwatch.ElapsedMilliseconds);
}
```

---

## ?? Key Takeaways

1. **`nameof(List<>)`** returns `"List"` (not `"List`1"`)
2. **`nameof(Dictionary<,>)`** returns `"Dictionary"` (not `"Dictionary`2"`)
3. **Cleaner logging** - No more backticks in logs
4. **Better error messages** - More professional and readable
5. **Type documentation** - Clear type names in documentation
6. **Cache keys** - Clean, readable cache key generation
7. **Diagnostics** - Better diagnostic messages

---

## ?? Important Notes

### What `nameof` Returns
- `nameof(List<>)` ? `"List"` (base type name)
- `nameof(List<int>)` ? `"List"` (also base type name)
- `typeof(List<>).Name` ? `"List`1"` (includes arity)
- `typeof(List<int>).Name` ? `"List`1"` (still includes arity)

### Type Parameter Syntax
- **One parameter**: `List<>`, `Task<>`, `IEnumerable<>`
- **Two parameters**: `Dictionary<,>`, `Func<,>`, `KeyValuePair<,>`
- **Three parameters**: `Func<,,>`, `Action<,,>`

---

## ?? Next Steps

Try using `nameof` with unbound generics in your code:
1. Replace `typeof().Name` calls in logging
2. Update error messages
3. Clean up cache key generation
4. Improve API documentation

---

**Implemented in**: C#14.NET10POC  
**Feature Status**: ? Fully Implemented  
**API Endpoints**: `/api/NameofDemo/*`  
**Documentation**: This file
