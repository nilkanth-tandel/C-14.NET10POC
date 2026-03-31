using C_14.NET10POC.Application.Extensions;
using C_14.NET10POC.Domain.Entities;

namespace C_14.NET10POC.Application.Services;

/// <summary>
/// Service demonstrating C# 14 Extension Members usage
/// </summary>
public interface IExtensionDemoService
{
    Task<ExtensionDemoResult> DemonstrateProductExtensions();
    Task<ExtensionDemoResult> DemonstrateCategoryExtensions();
    Task<ExtensionDemoResult> DemonstrateStaticExtensions();
}

public record ExtensionDemoResult(
    string FeatureName,
    Dictionary<string, object> Results,
    string Description
);
