using C_14.NET10POC.Application.Interfaces;
using C_14.NET10POC.Application.Services;
using C_14.NET10POC.Infrastructure.Data;
using C_14.NET10POC.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IExtensionDemoService, ExtensionDemoService>();
builder.Services.AddScoped<INameofDemoService, NameofDemoService>();
builder.Services.AddScoped<ISpanDemoService, SpanDemoService>();
builder.Services.AddScoped<IPartialPropertiesService, PartialPropertiesService>();
builder.Services.AddScoped<ICompoundOperatorService, CompoundOperatorService>();

// Configure OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    // Configure Scalar for API documentation (replacement for Swagger)
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle(".NET 10 & C# 14 Features POC")
            .WithTheme(ScalarTheme.Purple)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
