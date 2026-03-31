using C_14.NET10POC.Application.Services;
using C_14.NET10POC.Domain.Entities;
using System.Diagnostics;

namespace C_14.NET10POC.Infrastructure.Services;

/// <summary>
/// Implementation demonstrating C# 14 implicit span conversions
/// </summary>
public class SpanDemoService : ISpanDemoService
{
    public SpanDemoResult DemonstrateImplicitConversions()
    {
        var examples = new Dictionary<string, object>();

        // C# 14 Feature: Implicit conversions to Span<T> and ReadOnlySpan<T>

        // 1. String to ReadOnlySpan<char> (implicit!)
        string text = "Hello, C# 14!";
        ReadOnlySpan<char> spanFromString = text; // C# 14: Implicit conversion!
        examples["String to ReadOnlySpan<char>"] = new
        {
            Original = text,
            SpanLength = spanFromString.Length,
            FirstChar = spanFromString[0],
            Feature = "Implicit conversion from string"
        };

        // 2. Array to Span<T> (implicit!)
        int[] numbers = { 1, 2, 3, 4, 5 };
        Span<int> spanFromArray = numbers; // C# 14: Implicit conversion!
        examples["Array to Span<int>"] = new
        {
            ArrayLength = numbers.Length,
            SpanLength = spanFromArray.Length,
            CanModify = true,
            Feature = "Implicit conversion from array"
        };

        // 3. Array to ReadOnlySpan<T> (implicit!)
        ReadOnlySpan<int> readOnlySpan = numbers; // C# 14: Implicit conversion!
        examples["Array to ReadOnlySpan<int>"] = new
        {
            ArrayLength = numbers.Length,
            SpanLength = readOnlySpan.Length,
            CanModify = false,
            Feature = "Implicit conversion to read-only"
        };

        // 4. Span slicing with implicit conversion
        Span<int> slice = spanFromArray[1..4]; // Gets elements 1, 2, 3
        examples["Span slicing"] = new
        {
            OriginalLength = spanFromArray.Length,
            SliceLength = slice.Length,
            SliceContents = slice.ToArray(),
            Feature = "Zero-copy slicing"
        };

        // 5. ReadOnlySpan from string literal (implicit!)
        ReadOnlySpan<char> literalSpan = "C# 14"; // C# 14: Direct implicit conversion!
        examples["String literal to Span"] = new
        {
            Length = literalSpan.Length,
            Value = literalSpan.ToString(),
            Feature = "Direct conversion from literal"
        };

        return new SpanDemoResult(
            "Implicit Span Conversions",
            examples,
            "Demonstrates C# 14 first-class support for Span<T> with implicit conversions"
        );
    }

    public SpanDemoResult DemonstrateStringOperations()
    {
        var examples = new Dictionary<string, object>();

        // C# 14: String operations with Span for better performance

        string productName = "  Sample Product Name  ";

        // 1. Trim with Span (zero-copy!)
        ReadOnlySpan<char> trimmedSpan = productName.AsSpan().Trim();
        examples["Trim operation"] = new
        {
            Original = productName,
            Trimmed = trimmedSpan.ToString(),
            MemoryAllocated = "Zero (stack-only)",
            Feature = "Zero-copy string trimming"
        };

        // 2. Substring with Span (zero-copy!)
        string fullName = "Product: Laptop Computer";
        ReadOnlySpan<char> productPart = fullName.AsSpan(9, 6); // "Laptop"
        examples["Substring operation"] = new
        {
            Original = fullName,
            Extracted = productPart.ToString(),
            MemoryAllocated = "Zero (stack-only)",
            Feature = "Zero-copy substring"
        };

        // 3. String comparison with Span (efficient!)
        string search1 = "laptop";
        string search2 = "Laptop";
        bool areEqual = search1.AsSpan().Equals(search2.AsSpan(), StringComparison.OrdinalIgnoreCase);
        examples["String comparison"] = new
        {
            String1 = search1,
            String2 = search2,
            AreEqual = areEqual,
            Feature = "Efficient case-insensitive comparison"
        };

        // 4. String contains with Span
        string description = "High-performance laptop with SSD";
        bool containsLaptop = description.AsSpan().Contains("laptop", StringComparison.OrdinalIgnoreCase);
        examples["Contains check"] = new
        {
            Text = description,
            SearchTerm = "laptop",
            Found = containsLaptop,
            Feature = "Efficient contains check"
        };

        // 5. String splitting with Span (memory-efficient!)
        string csv = "Product,Category,Price";
        var parts = new List<string>();
        foreach (var part in csv.AsSpan().Split(','))
        {
            parts.Add(csv[part].ToString());
        }
        examples["String splitting"] = new
        {
            Original = csv,
            Parts = parts,
            Feature = "Memory-efficient splitting"
        };

        return new SpanDemoResult(
            "String Operations with Span",
            examples,
            "Shows how Span<T> enables zero-copy, high-performance string operations"
        );
    }

    public SpanDemoResult DemonstrateArrayOperations()
    {
        var examples = new Dictionary<string, object>();

        // C# 14: Array operations with Span

        // 1. Array modification with Span
        int[] prices = { 100, 200, 300, 400, 500 };
        Span<int> priceSpan = prices; // Implicit conversion
        
        // Modify through span
        priceSpan[0] = 150;
        priceSpan[^1] = 550; // Last element
        
        examples["Array modification"] = new
        {
            OriginalPrices = new[] { 100, 200, 300, 400, 500 },
            ModifiedPrices = prices,
            Feature = "Direct array modification through span"
        };

        // 2. Array slicing without allocation
        int[] inventory = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
        Span<int> firstHalf = inventory.AsSpan(0, 5);
        Span<int> secondHalf = inventory.AsSpan(5);
        
        examples["Array slicing"] = new
        {
            FullArray = inventory,
            FirstHalf = firstHalf.ToArray(),
            SecondHalf = secondHalf.ToArray(),
            MemoryAllocated = "Zero for slicing",
            Feature = "Zero-copy array slicing"
        };

        // 3. Span operations on slice
        Span<int> middleSection = inventory.AsSpan(3, 4); // Elements 3-6
        middleSection.Reverse(); // Reverses in place!
        
        examples["In-place operations"] = new
        {
            ModifiedArray = inventory,
            ReversedSection = "Elements at index 3-6 reversed in original array",
            Feature = "In-place transformations"
        };

        // 4. Copying with spans
        int[] source = { 1, 2, 3, 4, 5 };
        int[] destination = new int[5];
        source.AsSpan().CopyTo(destination);
        
        examples["Array copying"] = new
        {
            Source = source,
            Destination = destination,
            Feature = "Efficient array copying"
        };

        // 5. Filling arrays with Span
        int[] buffer = new int[10];
        buffer.AsSpan().Fill(42);
        
        examples["Array filling"] = new
        {
            Array = buffer,
            FillValue = 42,
            Feature = "Fast array initialization"
        };

        return new SpanDemoResult(
            "Array Operations with Span",
            examples,
            "Demonstrates efficient array manipulations using Span<T>"
        );
    }

    public SpanPerformanceResult DemonstratePerformanceComparison()
    {
        var benchmarks = new Dictionary<string, PerformanceMetrics>();
        const long iterations = 100_000;

        // Benchmark 1: String Substring
        var (traditionalSubstring, spanSubstring) = BenchmarkSubstring(iterations);
        var substringImprovement = CalculateImprovement(traditionalSubstring, spanSubstring);
        benchmarks["String Substring"] = new PerformanceMetrics(
            iterations,
            traditionalSubstring,
            spanSubstring,
            substringImprovement,
            spanSubstring < traditionalSubstring ? "Span (Faster)" : "Traditional"
        );

        // Benchmark 2: String Trim
        var (traditionalTrim, spanTrim) = BenchmarkTrim(iterations);
        var trimImprovement = CalculateImprovement(traditionalTrim, spanTrim);
        benchmarks["String Trim"] = new PerformanceMetrics(
            iterations,
            traditionalTrim,
            spanTrim,
            trimImprovement,
            spanTrim < traditionalTrim ? "Span (Faster)" : "Traditional"
        );

        // Benchmark 3: Array Slicing
        var (traditionalSlice, spanSlice) = BenchmarkArraySlicing(iterations);
        var sliceImprovement = CalculateImprovement(traditionalSlice, spanSlice);
        benchmarks["Array Slicing"] = new PerformanceMetrics(
            iterations,
            traditionalSlice,
            spanSlice,
            sliceImprovement,
            spanSlice < traditionalSlice ? "Span (Faster)" : "Traditional"
        );

        // Benchmark 4: String Comparison
        var (traditionalCompare, spanCompare) = BenchmarkStringComparison(iterations);
        var compareImprovement = CalculateImprovement(traditionalCompare, spanCompare);
        benchmarks["String Comparison"] = new PerformanceMetrics(
            iterations,
            traditionalCompare,
            spanCompare,
            compareImprovement,
            spanCompare < traditionalCompare ? "Span (Faster)" : "Traditional"
        );

        return new SpanPerformanceResult(
            "Performance Comparison: Traditional vs Span",
            benchmarks,
            "Compares traditional string/array operations with Span<T> operations"
        );
    }

    #region Benchmark Methods

    private (double traditional, double span) BenchmarkSubstring(long iterations)
    {
        string text = "This is a sample product description for testing performance";

        var sw1 = Stopwatch.StartNew();
        for (long i = 0; i < iterations; i++)
        {
            var result = text.Substring(10, 20);
        }
        sw1.Stop();

        var sw2 = Stopwatch.StartNew();
        for (long i = 0; i < iterations; i++)
        {
            var result = text.AsSpan(10, 20);
        }
        sw2.Stop();

        return (sw1.Elapsed.TotalMilliseconds, sw2.Elapsed.TotalMilliseconds);
    }

    private (double traditional, double span) BenchmarkTrim(long iterations)
    {
        string text = "  Sample Product  ";

        var sw1 = Stopwatch.StartNew();
        for (long i = 0; i < iterations; i++)
        {
            var result = text.Trim();
        }
        sw1.Stop();

        var sw2 = Stopwatch.StartNew();
        for (long i = 0; i < iterations; i++)
        {
            var result = text.AsSpan().Trim();
        }
        sw2.Stop();

        return (sw1.Elapsed.TotalMilliseconds, sw2.Elapsed.TotalMilliseconds);
    }

    private (double traditional, double span) BenchmarkArraySlicing(long iterations)
    {
        int[] array = Enumerable.Range(1, 100).ToArray();

        var sw1 = Stopwatch.StartNew();
        for (long i = 0; i < iterations; i++)
        {
            var result = array.Skip(10).Take(20).ToArray();
        }
        sw1.Stop();

        var sw2 = Stopwatch.StartNew();
        for (long i = 0; i < iterations; i++)
        {
            var result = array.AsSpan(10, 20);
        }
        sw2.Stop();

        return (sw1.Elapsed.TotalMilliseconds, sw2.Elapsed.TotalMilliseconds);
    }

    private (double traditional, double span) BenchmarkStringComparison(long iterations)
    {
        string str1 = "Product";
        string str2 = "PRODUCT";

        var sw1 = Stopwatch.StartNew();
        for (long i = 0; i < iterations; i++)
        {
            var result = string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }
        sw1.Stop();

        var sw2 = Stopwatch.StartNew();
        for (long i = 0; i < iterations; i++)
        {
            var result = str1.AsSpan().Equals(str2, StringComparison.OrdinalIgnoreCase);
        }
        sw2.Stop();

        return (sw1.Elapsed.TotalMilliseconds, sw2.Elapsed.TotalMilliseconds);
    }

    private double CalculateImprovement(double traditional, double span)
    {
        if (traditional == 0) return 0;
        return Math.Round(((traditional - span) / traditional) * 100, 2);
    }

    #endregion
}
