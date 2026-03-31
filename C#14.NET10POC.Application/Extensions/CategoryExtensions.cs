using C_14.NET10POC.Domain.Entities;

namespace C_14.NET10POC.Application.Extensions;

/// <summary>
/// C# 14 Feature: Extension Members for Category collections
/// </summary>
public static class CategoryExtensions
{
    // C# 14 Feature: Extension block with instance extensions
    extension(IEnumerable<Category> categories)
    {
        /// <summary>
        /// Extension Property: Check if collection has any active categories
        /// </summary>
        public bool HasActiveCategories => categories.Any(c => c.IsActive);

        /// <summary>
        /// Extension Property: Get count of active categories
        /// </summary>
        public int ActiveCount => categories.Count(c => c.IsActive);

        /// <summary>
        /// Extension Property: Check if collection is empty
        /// </summary>
        public bool IsEmpty => !categories.Any();

        /// <summary>
        /// Extension Method: Filter active categories
        /// </summary>
        public IEnumerable<Category> WhereActive()
        {
            return categories.Where(c => c.IsActive);
        }

        /// <summary>
        /// Extension Method: Get categories with products
        /// </summary>
        public IEnumerable<Category> WithProducts()
        {
            return categories.Where(c => c.Products != null && c.Products.Any());
        }

        /// <summary>
        /// Extension Method: Get category names as comma-separated string
        /// </summary>
        public string ToCategoryList()
        {
            return string.Join(", ", categories.Select(c => c.Name));
        }
    }

    // C# 14 Feature: Extension block with static extensions
    extension(IEnumerable<Category>)
    {
        /// <summary>
        /// Static Extension Property: Empty collection
        /// </summary>
        public static IEnumerable<Category> Empty => Enumerable.Empty<Category>();

        /// <summary>
        /// Static Extension Method: Merge category collections
        /// </summary>
        public static IEnumerable<Category> Merge(
            IEnumerable<Category> first, 
            IEnumerable<Category> second)
        {
            return first.Concat(second).DistinctBy(c => c.Id);
        }
    }
}
