using C_14.NET10POC.Application.Models;
using C_14.NET10POC.Application.Services;

namespace C_14.NET10POC.Infrastructure.Services;

/// <summary>
/// Service demonstrating operator concepts
/// (User-defined compound assignment operators not yet available in C# 14 preview)
/// </summary>
public class CompoundOperatorService : ICompoundOperatorService
{
    public CompoundOperatorResult DemonstrateCompoundAssignment()
    {
        var examples = new Dictionary<string, object>();
        bool isAvailable = false;

        examples["Status"] = "User-defined compound assignment operators not yet available in .NET 10 preview";
        examples["Expected syntax (when available)"] = "public static Price operator +=(Price left, Price right)";
        examples["Current workaround"] = "Use regular operators: price = price + otherPrice";
        
        // Demonstrate current approach
        var price1 = new Price(100m, "USD");
        var price2 = new Price(50m, "USD");
        
        // Current way (creates new object)
        var result = price1 + price2;
        
        examples["Current usage"] = new
        {
            Price1 = price1.ToString(),
            Price2 = price2.ToString(),
            Result = result.ToString(),
            Method = "Using regular + operator"
        };

        return new CompoundOperatorResult(
            "User-defined Compound Assignment Operators",
            examples,
            "Feature not yet available in current .NET 10 preview",
            isAvailable
        );
    }

    public CompoundOperatorResult DemonstratePlusEquals()
    {
        var examples = new Dictionary<string, object>();
        
        var price1 = new Price(100m, "USD");
        var price2 = new Price(50m, "USD");
        
        var originalPrice1 = new Price(price1.Amount, price1.Currency);
        
        // Current approach: reassignment
        price1 = price1 + price2;
        
        examples["Without compound operator"] = new
        {
            Original = originalPrice1.ToString(),
            AddedValue = price2.ToString(),
            Result = price1.ToString(),
            Code = "price1 = price1 + price2;",
            Note = "Creates new Price object"
        };

        examples["Future with compound operator"] = new
        {
            ExpectedCode = "price1 += price2;",
            Benefit = "Could modify in-place without creating new object",
            Status = "Not yet available"
        };

        return new CompoundOperatorResult(
            "Plus-Equals Operator",
            examples,
            "Demonstrates current approach vs future compound operator",
            false
        );
    }

    public CompoundOperatorResult DemonstrateMinusEquals()
    {
        var examples = new Dictionary<string, object>();
        
        var price1 = new Price(100m, "USD");
        var price2 = new Price(30m, "USD");
        
        var originalPrice1 = new Price(price1.Amount, price1.Currency);
        
        // Current approach
        price1 = price1 - price2;
        
        examples["Without compound operator"] = new
        {
            Original = originalPrice1.ToString(),
            SubtractedValue = price2.ToString(),
            Result = price1.ToString(),
            Code = "price1 = price1 - price2;",
            Note = "Creates new Price object"
        };

        examples["Future with compound operator"] = new
        {
            ExpectedCode = "price1 -= price2;",
            Benefit = "Could modify in-place",
            Status = "Not yet available"
        };

        // Demonstrate multiplication
        var price3 = new Price(50m, "USD");
        var multiplier = 1.1m;
        price3 = price3 * multiplier;
        
        examples["Multiplication example"] = new
        {
            Original = "50.00",
            Multiplier = multiplier,
            Result = price3.ToString(),
            CurrentCode = "price = price * multiplier;",
            FutureCode = "price *= multiplier; (not available)"
        };

        return new CompoundOperatorResult(
            "Compound Subtraction and Multiplication",
            examples,
            "Shows current operators vs planned compound operators",
            false
        );
    }
}
