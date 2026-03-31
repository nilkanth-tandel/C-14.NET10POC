namespace C_14.NET10POC.Application.Models;

/// <summary>
/// Custom type for demonstrating regular operators
/// (Compound assignment operators not yet available in C# 14 preview)
/// </summary>
public class Price
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";

    public Price(decimal amount, string currency = "USD")
    {
        Amount = amount;
        Currency = currency;
    }

    // Regular + operator
    public static Price operator +(Price left, Price right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Cannot add prices with different currencies");
        
        return new Price(left.Amount + right.Amount, left.Currency);
    }

    // Regular - operator
    public static Price operator -(Price left, Price right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Cannot subtract prices with different currencies");
        
        return new Price(left.Amount - right.Amount, left.Currency);
    }

    // Regular * operator
    public static Price operator *(Price price, decimal multiplier)
    {
        return new Price(price.Amount * multiplier, price.Currency);
    }

    // Regular / operator
    public static Price operator /(Price price, decimal divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException();
        
        return new Price(price.Amount / divisor, price.Currency);
    }

    public override string ToString() => $"{Amount:C} {Currency}";
    
    public override bool Equals(object? obj)
    {
        return obj is Price price &&
               Amount == price.Amount &&
               Currency == price.Currency;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }
}
