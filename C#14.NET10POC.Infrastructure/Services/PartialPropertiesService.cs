using C_14.NET10POC.Application.Models;
using C_14.NET10POC.Application.Services;

namespace C_14.NET10POC.Infrastructure.Services;

/// <summary>
/// Implementation demonstrating C# 14 partial properties (if available)
/// </summary>
public class PartialPropertiesService : IPartialPropertiesService
{
    public PartialPropertiesResult DemonstratePartialProperties()
    {
        var examples = new Dictionary<string, object>();
        bool isAvailable = true;

        try
        {
            // Test if partial properties work
            var example = new PartialPropertyExample
            {
                Name = "  John Doe  ",
                Age = 30
            };

            examples["Basic usage"] = new
            {
                InputName = "  John Doe  ",
                OutputName = example.Name,
                Age = example.Age,
                Feature = "Partial properties with validation"
            };

            examples["Property validation"] = new
            {
                Description = "Age property validates non-negative values",
                Example = "Setting Age = -5 would throw ArgumentException"
            };

            examples["Property transformation"] = new
            {
                Description = "Name property automatically trims whitespace",
                InputExample = "  Spaces  ",
                OutputExample = "Spaces"
            };

        }
        catch (Exception ex)
        {
            isAvailable = false;
            examples["Error"] = $"Partial properties not available: {ex.Message}";
        }

        return new PartialPropertiesResult(
            "Partial Properties",
            examples,
            "Demonstrates C# 14 partial properties feature",
            isAvailable
        );
    }

    public PartialPropertiesResult DemonstratePartialMethods()
    {
        var examples = new Dictionary<string, object>();
        
        examples["Feature"] = "Partial methods allow method declaration and implementation in different parts";
        examples["Use case"] = "Code generation scenarios where generated code and custom code are separated";
        examples["Status"] = "Partial methods already exist in previous C# versions";

        return new PartialPropertiesResult(
            "Partial Methods",
            examples,
            "Partial methods were available before C# 14",
            true
        );
    }
}
