# Database Setup Commands

## Prerequisites
Ensure you have SQL Server Express installed and running.

## Entity Framework Core Tools

If not already installed globally:
```cmd
dotnet tool install --global dotnet-ef
```

## Create Initial Migration

From the root solution directory:

```cmd
rem Create the initial migration
dotnet ef migrations add InitialCreate --project C#14.NET10POC.Infrastructure --startup-project C#14.NET10POC

rem Apply migration to create database
dotnet ef database update --project C#14.NET10POC.Infrastructure --startup-project C#14.NET10POC
```

## Useful Commands

### View migration status
```cmd
dotnet ef migrations list --project C#14.NET10POC.Infrastructure --startup-project C#14.NET10POC
```

### Remove last migration (if not applied)
```cmd
dotnet ef migrations remove --project C#14.NET10POC.Infrastructure --startup-project C#14.NET10POC
```

### Drop database
```cmd
dotnet ef database drop --project C#14.NET10POC.Infrastructure --startup-project C#14.NET10POC
```

### Generate SQL script
```cmd
dotnet ef migrations script --project C#14.NET10POC.Infrastructure --startup-project C#14.NET10POC -o migration.sql
```

## Connection String

Default connection string in `appsettings.json`:
```
Server=localhost\\SQLEXPRESS;Database=Net10FeaturesDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true
```

If your SQL Server instance is different, update the connection string accordingly.

## Verify Database Creation

After running `dotnet ef database update`, verify:
1. Database `Net10FeaturesDB` is created
2. Tables: `Categories`, `Products`
3. Seed data is populated (3 categories, 3 products)

## Run the Application

```cmd
cd C#14.NET10POC
dotnet run
```

## Access the Application

Once running, the application will display the URLs:
```
Now listening on: https://localhost:7xxx
Now listening on: http://localhost:5xxx
```

### API Documentation (Scalar)
Navigate to: **https://localhost:7xxx/scalar/v1**

This will open the Scalar API documentation interface where you can:
- View all API endpoints
- Test API calls interactively
- See request/response schemas
- Try out the API without external tools

### Direct API Access
- Products: https://localhost:7xxx/api/products
- Categories: https://localhost:7xxx/api/categories

Replace `7xxx` with the actual HTTPS port number shown in your console.
