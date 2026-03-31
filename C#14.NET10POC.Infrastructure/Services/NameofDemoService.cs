using C_14.NET10POC.Application.Services;
using C_14.NET10POC.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace C_14.NET10POC.Infrastructure.Services;

/// <summary>
/// Implementation demonstrating C# 14 nameof with unbound generic types
/// </summary>
public class NameofDemoService : INameofDemoService
{
    private readonly ILogger<NameofDemoService> _logger;

    public NameofDemoService(ILogger<NameofDemoService> logger)
    {
        _logger = logger;
    }

    public NameofDemoResult DemonstrateUnboundGenerics()
    {
        var examples = new Dictionary<string, string>();

        // C# 14 Feature: nameof with unbound generic types
        // Before C# 14: Not possible - had to use typeof().Name which includes backticks
        // After C# 14: Clean type name without type arguments

        // Collections
        examples["List<> (unbound)"] = nameof(List<>);
        examples["Dictionary<,> (unbound)"] = nameof(Dictionary<,>);
        examples["IEnumerable<> (unbound)"] = nameof(IEnumerable<>);
        examples["ICollection<> (unbound)"] = nameof(ICollection<>);
        examples["HashSet<> (unbound)"] = nameof(HashSet<>);
        examples["Queue<> (unbound)"] = nameof(Queue<>);
        examples["Stack<> (unbound)"] = nameof(Stack<>);

        // Custom types
        examples["Nullable<> (unbound)"] = nameof(Nullable<>);
        examples["Task<> (unbound)"] = nameof(Task<>);
        examples["ValueTask<> (unbound)"] = nameof(ValueTask<>);
        examples["Func<> (unbound)"] = nameof(Func<>);
        examples["Action<> (unbound)"] = nameof(Action<>);

        // Comparison with old way
        examples["typeof(List<>).Name (old way)"] = typeof(List<>).Name; // Returns "List`1" with backtick
        examples["nameof(List<>) (C# 14)"] = nameof(List<>); // Returns clean "List"

        return new NameofDemoResult(
            "nameof with Unbound Generic Types",
            examples,
            "Demonstrates C# 14 ability to use nameof with unbound generic types like List<>, Dictionary<,>, etc."
        );
    }

    public NameofDemoResult DemonstrateLoggingScenarios()
    {
        var examples = new Dictionary<string, string>();

        // C# 14 Feature: Clean logging with generic type names
        
        // Scenario 1: Repository pattern logging
        var repositoryLog = $"Initializing {nameof(IEnumerable<>)} repository for {nameof(Product)}";
        examples["Repository initialization"] = repositoryLog;

        // Scenario 2: Cache key generation
        var cacheKey = $"Cache_{nameof(List<>)}_{nameof(Product)}_InStock";
        examples["Cache key"] = cacheKey;

        // Scenario 3: Collection type logging
        var collectionLog = $"Converting {nameof(IEnumerable<>)} to {nameof(List<>)}";
        examples["Collection conversion"] = collectionLog;

        // Scenario 4: Generic method logging
        var methodLog = $"Calling generic method with {nameof(Dictionary<,>)} parameter";
        examples["Generic method call"] = methodLog;

        // Scenario 5: Type factory logging
        var factoryLog = $"Creating instance of {nameof(Task<>)} for {nameof(Product)} query";
        examples["Factory creation"] = factoryLog;

        // Scenario 6: Comparison logging
        examples["Old way with typeof"] = $"Working with {typeof(List<>).Name}"; // List`1
        examples["New way with nameof"] = $"Working with {nameof(List<>)}"; // List

        // Log an actual example
        _logger.LogInformation("C# 14 Feature: Using {CollectionType} for {EntityType}", 
            nameof(IEnumerable<>), 
            nameof(Product));

        return new NameofDemoResult(
            "nameof in Logging Scenarios",
            examples,
            "Shows practical logging scenarios using nameof with unbound generics for cleaner log messages"
        );
    }

    public NameofDemoResult DemonstrateErrorMessages()
    {
        var examples = new Dictionary<string, string>();

        // C# 14 Feature: Better error messages with generic types

        // Scenario 1: Validation error
        var validationError = $"Expected {nameof(IEnumerable<>)} but received null for {nameof(Product)} collection";
        examples["Validation error"] = validationError;

        // Scenario 2: Type mismatch error
        var typeMismatchError = $"Cannot convert {nameof(List<>)} to {nameof(Dictionary<,>)}";
        examples["Type mismatch"] = typeMismatchError;

        // Scenario 3: Null reference error
        var nullRefError = $"Null reference exception in {nameof(Task<>)} returning {nameof(Product)}";
        examples["Null reference"] = nullRefError;

        // Scenario 4: Configuration error
        var configError = $"Missing configuration for {nameof(ICollection<>)} of type {nameof(Category)}";
        examples["Configuration error"] = configError;

        // Scenario 5: Serialization error
        var serializationError = $"Failed to serialize {nameof(Dictionary<,>)} containing {nameof(Product)} entities";
        examples["Serialization error"] = serializationError;

        // Scenario 6: Generic constraint error
        var constraintError = $"Type constraint violation: {nameof(IEnumerable<>)} must be reference type";
        examples["Constraint error"] = constraintError;

        // Comparison
        examples["Old error (with typeof)"] = $"Error in {typeof(List<>).Name} operation"; // List`1
        examples["New error (with nameof)"] = $"Error in {nameof(List<>)} operation"; // List

        return new NameofDemoResult(
            "nameof in Error Messages",
            examples,
            "Demonstrates using nameof with unbound generics for clearer error messages"
        );
    }

    public NameofDemoResult DemonstrateTypeComparison()
    {
        var examples = new Dictionary<string, string>();

        // C# 14 Feature: Type name comparison and documentation

        // Scenario 1: Type registry
        var typeRegistry = new Dictionary<string, string>
        {
            [nameof(List<>)] = "Standard mutable collection",
            [nameof(IEnumerable<>)] = "Read-only sequence",
            [nameof(Dictionary<,>)] = "Key-value pair collection",
            [nameof(HashSet<>)] = "Unique element collection"
        };

        foreach (var kvp in typeRegistry)
        {
            examples[$"Type: {kvp.Key}"] = kvp.Value;
        }

        // Scenario 2: Generic type documentation
        examples["Documentation for List<>"] = $"See documentation for {nameof(List<>)} implementation";
        examples["Documentation for Dictionary<,>"] = $"See documentation for {nameof(Dictionary<,>)} usage";

        // Scenario 3: Type switching logic
        var typeSwitch = $"Switching between {nameof(IEnumerable<>)}, {nameof(ICollection<>)}, and {nameof(List<>)}";
        examples["Type switching"] = typeSwitch;

        // Scenario 4: Generic constraint description
        examples["Constraint: IEnumerable<>"] = $"Must implement {nameof(IEnumerable<>)} interface";
        examples["Constraint: ICollection<>"] = $"Must implement {nameof(ICollection<>)} interface";

        // Scenario 5: Comparison table
        examples["typeof(List<>).Name"] = typeof(List<>).Name; // "List`1" - includes backtick and arity
        examples["nameof(List<>)"] = nameof(List<>); // "List" - clean name
        examples["typeof(Dictionary<,>).Name"] = typeof(Dictionary<,>).Name; // "Dictionary`2"
        examples["nameof(Dictionary<,>)"] = nameof(Dictionary<,>); // "Dictionary"

        return new NameofDemoResult(
            "Type Comparison and Documentation",
            examples,
            "Shows how nameof provides cleaner type names compared to typeof().Name"
        );
    }
}
