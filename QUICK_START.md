# Quick Start - C# 14 & .NET 10 POC

> **Goal**: Get the POC running in 5 minutes

---

## ? 3-Step Setup

### 1. Database
```cmd
dotnet ef database update --project C#14.NET10POC.Infrastructure --startup-project C#14.NET10POC
```

### 2. Run
```cmd
cd C#14.NET10POC
dotnet run
```

### 3. Test
```cmd
start https://localhost:7xxx/scalar/v1
```

**Done!** ??

---

## ?? Test Features

Open Scalar and try these endpoints:

1. **Core API**: `GET /api/products`
2. **Extension Members**: `GET /api/ExtensionDemo/all`
3. **Performance**: `GET /api/SpanDemo/performance-benchmark` ?
4. **nameof Demo**: `GET /api/NameofDemo/type-comparison`

---

## ?? Expected Results

- **Products**: 3 seeded products
- **Categories**: 3 seeded categories
- **Extensions**: See extension properties in action
- **Performance**: 86-97% improvements with Span
- **nameof**: Clean type names (no backticks)

---

## ?? Troubleshooting

### Port Already in Use?
Check console output for actual port, or edit `appsettings.json`

### Database Issues?
```cmd
# Reset database
dotnet ef database drop --project C#14.NET10POC.Infrastructure --startup-project C#14.NET10POC
dotnet ef database update --project C#14.NET10POC.Infrastructure --startup-project C#14.NET10POC
```

### Build Errors?
```cmd
dotnet clean
dotnet restore
dotnet build
```

---

## ?? Next Steps

1. **[Master Guide](MASTER_GUIDE.md)** - Complete documentation
2. **[Features Reference](docs/FEATURES_REFERENCE.md)** - Feature details
3. **[API Reference](docs/API_REFERENCE.md)** - All endpoints

---

**Time to first request**: <5 minutes ??
