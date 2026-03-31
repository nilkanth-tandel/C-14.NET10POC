# API Reference - All Endpoints

## ?? Base URL

`https://localhost:7xxx` (replace `7xxx` with your port)

**API Docs**: `https://localhost:7xxx/scalar/v1`

---

## ?? Products API

### List All Products
```http
GET /api/products
```
**Response**: `200 OK` - Array of products

### Get Product by ID
```http
GET /api/products/{id}
```
**Response**: `200 OK` - Single product | `404 Not Found`

### Get Products by Category
```http
GET /api/products/category/{categoryId}
```

### Get In-Stock Products
```http
GET /api/products/in-stock
```

### Create Product
```http
POST /api/products
Content-Type: application/json

{
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 1299.99,
  "stockQuantity": 10,
  "categoryId": 1
}
```
**Response**: `201 Created`

### Update Product
```http
PUT /api/products/{id}
Content-Type: application/json

{
  "name": "Updated Name",
  "price": 1499.99
}
```

### Delete Product
```http
DELETE /api/products/{id}
```
**Response**: `204 No Content` | `404 Not Found`

---

## ?? Categories API

### List All Categories
```http
GET /api/categories
```

### Get Category by ID
```http
GET /api/categories/{id}
```

### Create Category
```http
POST /api/categories
Content-Type: application/json

{
  "name": "Electronics",
  "description": "Electronic devices"
}
```

### Update Category
```http
PUT /api/categories/{id}
Content-Type: application/json

{
  "name": "Updated Category",
  "description": "New description"
}
```

### Delete Category
```http
DELETE /api/categories/{id}
```

---

## ?? Extension Members Demo

### Product Extensions
```http
GET /api/ExtensionDemo/product-extensions
```
**Shows**: Extension properties, methods

### Category Extensions
```http
GET /api/ExtensionDemo/category-extensions
```

### Static Extensions & Operators
```http
GET /api/ExtensionDemo/static-extensions
```
**Shows**: Static extensions, custom `+` operator

### All Extension Demos
```http
GET /api/ExtensionDemo/all
```

---

## ??? nameof Demo

### Unbound Generic Types
```http
GET /api/NameofDemo/unbound-generics
```
**Shows**: `nameof(List<>)` ? "List"

### Logging Scenarios
```http
GET /api/NameofDemo/logging-scenarios
```

### Error Messages
```http
GET /api/NameofDemo/error-messages
```

### Type Comparison
```http
GET /api/NameofDemo/type-comparison
```
**Shows**: `nameof` vs `typeof().Name`

### All nameof Demos
```http
GET /api/NameofDemo/all
```

### Live Logging Example
```http
POST /api/NameofDemo/log-example
Content-Type: application/json

{
  "customMessage": "Testing nameof"
}
```

---

## ? Span Performance Demo

### Implicit Conversions
```http
GET /api/SpanDemo/implicit-conversions
```
**Shows**: String/array to Span conversions

### String Operations
```http
GET /api/SpanDemo/string-operations
```
**Shows**: Zero-copy trim, substring, etc.

### Array Operations
```http
GET /api/SpanDemo/array-operations
```

### Performance Benchmark ?
```http
GET /api/SpanDemo/performance-benchmark
```
**Shows**: 86-97% performance improvements

### Live Example
```http
POST /api/SpanDemo/process-product-name
Content-Type: application/json

{
  "productName": "  Sample Product  "
}
```

### All Span Demos
```http
GET /api/SpanDemo/all
```

---

## ?? Partial Properties Demo

### Partial Properties
```http
GET /api/PartialPropertiesDemo/partial-properties
```

### Partial Methods
```http
GET /api/PartialPropertiesDemo/partial-methods
```

### All Partial Demos
```http
GET /api/PartialPropertiesDemo/all
```

---

## ? Compound Operators Demo

### Compound Assignment
```http
GET /api/CompoundOperatorDemo/compound-assignment
```
**Shows**: Status of user-defined `+=`, `-=`, etc.

### Plus-Equals Operator
```http
GET /api/CompoundOperatorDemo/plus-equals
```

### Minus-Equals Operator
```http
GET /api/CompoundOperatorDemo/minus-equals
```

### All Compound Demos
```http
GET /api/CompoundOperatorDemo/all
```

---

## ?? Response Formats

All responses are JSON format.

### Success Responses
- `200 OK` - Request successful
- `201 Created` - Resource created
- `204 No Content` - Delete successful

### Error Responses
- `400 Bad Request` - Invalid input
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

---

## ?? Quick Test Sequence

1. **Get all products**: `GET /api/products`
2. **Test extension members**: `GET /api/ExtensionDemo/all`
3. **See performance**: `GET /api/SpanDemo/performance-benchmark`
4. **Test nameof**: `GET /api/NameofDemo/type-comparison`

---

## ?? Performance Benchmarks

Available at: `GET /api/SpanDemo/performance-benchmark`

**Expected Results**:
- String Substring: **86% faster**
- String Trim: **91% faster**
- Array Slicing: **97% faster**

---

**Total Endpoints**: 30+  
**Categories**: 6 (Products, Categories, 4 Demo APIs)  
**All Working**: ?
