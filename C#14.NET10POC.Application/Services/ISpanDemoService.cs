namespace C_14.NET10POC.Application.Services;

/// <summary>
/// Service demonstrating C# 14 implicit span conversions
/// </summary>
public interface ISpanDemoService
{
    SpanDemoResult DemonstrateImplicitConversions();
    SpanDemoResult DemonstrateStringOperations();
    SpanDemoResult DemonstrateArrayOperations();
    SpanPerformanceResult DemonstratePerformanceComparison();
}

public record SpanDemoResult(
    string FeatureName,
    Dictionary<string, object> Examples,
    string Description
);

public record SpanPerformanceResult(
    string FeatureName,
    Dictionary<string, PerformanceMetrics> Benchmarks,
    string Description
);

public record PerformanceMetrics(
    long IterationCount,
    double TraditionalMs,
    double SpanMs,
    double ImprovementPercent,
    string Winner
);
