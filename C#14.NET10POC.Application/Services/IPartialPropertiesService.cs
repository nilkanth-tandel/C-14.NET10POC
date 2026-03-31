namespace C_14.NET10POC.Application.Services;

/// <summary>
/// Service demonstrating C# 14 partial properties (if available)
/// </summary>
public interface IPartialPropertiesService
{
    PartialPropertiesResult DemonstratePartialProperties();
    PartialPropertiesResult DemonstratePartialMethods();
}

public record PartialPropertiesResult(
    string FeatureName,
    Dictionary<string, object> Examples,
    string Description,
    bool IsAvailable
);
