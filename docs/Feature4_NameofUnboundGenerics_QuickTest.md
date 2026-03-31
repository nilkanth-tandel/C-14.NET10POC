# ?? Quick Test Guide - C# 14 `nameof` with Unbound Generic Types

## Run the Application

```cmd
cd C#14.NET10POC
dotnet run
```

## Access Scalar API Documentation

Navigate to: `https://localhost:7xxx/scalar/v1`

---

## ?? Test `nameof` Feature

### 1. Unbound Generic Types

**Endpoint**: `GET /api/NameofDemo/unbound-generics`

**What it demonstrates**:
- ? `nameof(List<>)` ? Returns "List" (not "List`1")
- ? `nameof(Dictionary<,>)` ? Returns "Dictionary" (not "Dictionary`2")
- ? `nameof(IEnumerable<>)` ? Returns "IEnumerable"
- ? Comparison with old `typeof().Name` approach

**Try it in Scalar**:
1. Find "Nameof Demo" section
2. Click on "unbound-generics"
3. Click "Try it out"
4. Click "Execute"

---

### 2. Logging Scenarios

**Endpoint**: `GET /api/NameofDemo/logging-scenarios`

**What it demonstrates**:
- ? Repository initialization logging
- ? Cache key generation
- ? Collection type logging
- ? Generic method call logging
- ? Clean vs. ugly output comparison

---

### 3. Error Messages

**Endpoint**: `GET /api/NameofDemo/error-messages`

**What it demonstrates**:
- ? Validation errors with clean type names
- ? Type mismatch errors
- ? Null reference errors
- ? Configuration errors
- ? Serialization errors

---

### 4. Type Comparison

**Endpoint**: `GET /api/NameofDemo/type-comparison`

**What it demonstrates**:
- ? Side-by-side comparison:
  - `typeof(List<>).Name` ? "List`1" (ugly)
  - `nameof(List<>)` ? "List" (clean)

---

### 5. Live Logging Example

**Endpoint**: `POST /api/NameofDemo/log-example`

**Body** (optional):
```json
{
  "customMessage": "Testing C# 14 nameof feature"
}
```

**What it does**:
- ? Actually logs a message using C# 14 nameof
- ? Check your console/application logs to see clean output!
- ? Returns comparison of old vs. new approach

---

### 6. All Demonstrations

**Endpoint**: `GET /api/NameofDemo/all`

Returns all four demonstrations in one response.

---

## ?? What Makes This Special?

### Before C# 14:
```csharp
// ? Not allowed - compiler error
// var name = nameof(List<>);

// Had to use typeof().Name - ugly!
var name = typeof(List<>).Name;        // "List`1" ??
var name2 = typeof(Dictionary<,>).Name; // "Dictionary`2" ??

_logger.LogInformation($"Using {typeof(List<>).Name}");
// Output: "Using List`1" ??
```

### With C# 14:
```csharp
// ? Clean and simple!
var name = nameof(List<>);           // "List" ?
var name2 = nameof(Dictionary<,>);   // "Dictionary" ?

_logger.LogInformation($"Using {nameof(List<>)}");
// Output: "Using List" ?
```

---

## ?? Expected Results

When you call `/api/NameofDemo/unbound-generics`, you should see:

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

**Notice**: 
- Old way has backtick: `"List`1"`
- New way is clean: `"List"`

---

## ?? Real Examples

### Logging
```csharp
// Old way (ugly)
_logger.LogInformation("Using " + typeof(List<>).Name);
// Output: "Using List`1"

// C# 14 (clean)
_logger.LogInformation($"Using {nameof(List<>)}");
// Output: "Using List"
```

### Error Messages
```csharp
// Old way (ugly)
throw new Exception($"Cannot convert {typeof(List<>).Name}");
// Error: "Cannot convert List`1"

// C# 14 (clean)
throw new Exception($"Cannot convert {nameof(List<>)}");
// Error: "Cannot convert List"
```

### Cache Keys
```csharp
// Old way (ugly)
var key = $"Cache_{typeof(List<>).Name}_Products";
// Key: "Cache_List`1_Products"

// C# 14 (clean)
var key = $"Cache_{nameof(List<>)}_Products";
// Key: "Cache_List_Products"
```

---

## ? Checklist

- [ ] Application is running
- [ ] Accessed Scalar at `/scalar/v1`
- [ ] Tested unbound-generics endpoint
- [ ] Tested logging-scenarios endpoint
- [ ] Tested error-messages endpoint
- [ ] Tested type-comparison endpoint
- [ ] Tested log-example endpoint (POST)
- [ ] Checked application logs for clean output
- [ ] Understood the difference between old and new approach

---

## ?? Key Learnings

1. **Clean Names** - `nameof(List<>)` returns "List" (not "List`1")
2. **Multiple Parameters** - `nameof(Dictionary<,>)` returns "Dictionary"
3. **Better Logging** - No more backticks in log messages
4. **Professional Errors** - Error messages look much better
5. **Simple Syntax** - Just use `nameof(Type<>)` or `nameof(Type<,>)`

---

## ?? Compare Output

| Expression | Output |
|------------|--------|
| `typeof(List<>).Name` | `"List`1"` (ugly) |
| `nameof(List<>)` | `"List"` (clean) ? |
| `typeof(Dictionary<,>).Name` | `"Dictionary`2"` (ugly) |
| `nameof(Dictionary<,>)` | `"Dictionary"` (clean) ? |

---

## ?? More Information

See `docs/Feature4_NameofUnboundGenerics_Guide.md` for complete documentation.

---

**Feature Status**: ? Fully Implemented  
**C# Version**: 14.0  
**.NET Version**: 10.0  
**Previous Feature**: Extension Members  
**Next Feature**: Implicit Span Conversions
