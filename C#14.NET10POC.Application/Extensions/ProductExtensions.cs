using C_14.NET10POC.Domain.Entities;

namespace C_14.NET10POC.Application.Extensions;

/// <summary>
/// C# 14 Feature: Extension Members for Product collections
/// Demonstrates extension properties, methods, and static extensions
/// </summary>
public static class ProductExtensions
{
    // C# 14 Feature: Extension block with instance extensions
    extension(IEnumerable<Product> products)
    {
        /// <summary>
        /// Extension Property: Check if collection has any in-stock items
        /// </summary>
        public bool HasInStockItems => products.Any(p => p.IsInStock);

        /// <summary>
        /// Extension Property: Get total inventory value
        /// </summary>
        public decimal TotalInventoryValue => products.Sum(p => p.Price * p.StockQuantity);

        /// <summary>
        /// Extension Property: Check if collection is empty
        /// </summary>
        public bool IsEmpty => !products.Any();

        /// <summary>
        /// Extension Property: Count of in-stock products
        /// </summary>
        public int InStockCount => products.Count(p => p.IsInStock);

        /// <summary>
        /// Extension Method: Filter products that are in stock
        /// </summary>
        public IEnumerable<Product> WhereInStock()
        {
            return products.Where(p => p.IsInStock);
        }

        /// <summary>
        /// Extension Method: Filter products by price range
        /// </summary>
        public IEnumerable<Product> WherePriceRange(decimal minPrice, decimal maxPrice)
        {
            return products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
        }

        /// <summary>
        /// Extension Method: Get products low on stock
        /// </summary>
        public IEnumerable<Product> WhereLowStock(int threshold = 10)
        {
            return products.Where(p => p.StockQuantity > 0 && p.StockQuantity <= threshold);
        }

        /// <summary>
        /// Extension Method: Get total value of products
        /// </summary>
        public decimal CalculateTotalValue()
        {
            return products.Sum(p => p.Price * p.StockQuantity);
        }
    }

    // C# 14 Feature: Extension block with static extensions
    extension(IEnumerable<Product>)
    {
        /// <summary>
        /// Static Extension Property: Empty collection
        /// </summary>
        public static IEnumerable<Product> Empty => Enumerable.Empty<Product>();

        /// <summary>
        /// Static Extension Method: Combine two product collections
        /// </summary>
        public static IEnumerable<Product> Combine(
            IEnumerable<Product> first, 
            IEnumerable<Product> second)
        {
            return first.Concat(second).DistinctBy(p => p.Id);
        }

        /// <summary>
        /// Static Extension Method: Create a sample product collection
        /// </summary>
        public static IEnumerable<Product> CreateSampleCollection(int count = 5)
        {
            var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            
            return Enumerable.Range(1, count).Select(i => new Product
            {
                Id = i,
                Name = $"Sample Product {i}",
                Description = $"Sample description for product {i}",
                Price = 10m * i,
                StockQuantity = i * 5,
                CategoryId = (i % 3) + 1,
                IsActive = true,
                CreatedAt = seedDate
            });
        }

        /// <summary>
        /// Static Extension Operator: Concatenate two product collections using +
        /// </summary>
        public static IEnumerable<Product> operator +(
            IEnumerable<Product> left, 
            IEnumerable<Product> right)
        {
            return left.Concat(right);
        }
    }
}
