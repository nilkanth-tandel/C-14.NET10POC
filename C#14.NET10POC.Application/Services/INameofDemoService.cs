namespace C_14.NET10POC.Application.Services;

/// <summary>
/// Service demonstrating C# 14 nameof with unbound generic types
/// </summary>
public interface INameofDemoService
{
    NameofDemoResult DemonstrateUnboundGenerics();
    NameofDemoResult DemonstrateLoggingScenarios();
    NameofDemoResult DemonstrateErrorMessages();
    NameofDemoResult DemonstrateTypeComparison();
}

public record NameofDemoResult(
    string FeatureName,
    Dictionary<string, string> Examples,
    string Description
);
