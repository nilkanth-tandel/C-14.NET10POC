namespace C_14.NET10POC.Application.Services;

/// <summary>
/// Service demonstrating C# 14 user-defined compound assignment operators
/// </summary>
public interface ICompoundOperatorService
{
    CompoundOperatorResult DemonstrateCompoundAssignment();
    CompoundOperatorResult DemonstratePlusEquals();
    CompoundOperatorResult DemonstrateMinusEquals();
}

public record CompoundOperatorResult(
    string FeatureName,
    Dictionary<string, object> Examples,
    string Description,
    bool IsAvailable
);
