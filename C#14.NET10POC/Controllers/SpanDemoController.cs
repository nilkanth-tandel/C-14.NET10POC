using C_14.NET10POC.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace C_14.NET10POC.Controllers;

/// <summary>
/// Controller demonstrating C# 14 implicit span conversions
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SpanDemoController : ControllerBase
{
    private readonly ISpanDemoService _demoService;
    private readonly ILogger<SpanDemoController> _logger;

    public SpanDemoController(
        ISpanDemoService demoService,
        ILogger<SpanDemoController> logger)
    {
        _demoService = demoService;
        _logger = logger;
    }

    /// <summary>
    /// Demonstrate C# 14 implicit span conversions
    /// </summary>
    /// <remarks>
    /// Shows implicit conversions:
    /// - String to ReadOnlySpan&lt;char&gt;
    /// - Array to Span&lt;T&gt;
    /// - Array to ReadOnlySpan&lt;T&gt;
    /// - String literal to Span
    /// - Zero-copy slicing
    /// </remarks>
    [HttpGet("implicit-conversions")]
    [ProducesResponseType(typeof(SpanDemoResult), StatusCodes.Status200OK)]
    public ActionResult<SpanDemoResult> GetImplicitConversions()
    {
        _logger.LogInformation("Demonstrating C# 14 implicit span conversions");
        var result = _demoService.DemonstrateImplicitConversions();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate string operations with Span for better performance
    /// </summary>
    /// <remarks>
    /// Shows:
    /// - Zero-copy string trimming
    /// - Zero-copy substring
    /// - Efficient string comparison
    /// - Contains check with Span
    /// - Memory-efficient string splitting
    /// </remarks>
    [HttpGet("string-operations")]
    [ProducesResponseType(typeof(SpanDemoResult), StatusCodes.Status200OK)]
    public ActionResult<SpanDemoResult> GetStringOperations()
    {
        _logger.LogInformation("Demonstrating string operations with Span");
        var result = _demoService.DemonstrateStringOperations();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate array operations with Span
    /// </summary>
    /// <remarks>
    /// Shows:
    /// - Array modification through span
    /// - Zero-copy array slicing
    /// - In-place transformations
    /// - Efficient array copying
    /// - Fast array initialization
    /// </remarks>
    [HttpGet("array-operations")]
    [ProducesResponseType(typeof(SpanDemoResult), StatusCodes.Status200OK)]
    public ActionResult<SpanDemoResult> GetArrayOperations()
    {
        _logger.LogInformation("Demonstrating array operations with Span");
        var result = _demoService.DemonstrateArrayOperations();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate performance comparison: Traditional vs Span
    /// </summary>
    /// <remarks>
    /// Benchmarks:
    /// - String Substring (100,000 iterations)
    /// - String Trim (100,000 iterations)
    /// - Array Slicing (100,000 iterations)
    /// - String Comparison (100,000 iterations)
    /// 
    /// Shows performance improvement percentage with Span
    /// </remarks>
    [HttpGet("performance-benchmark")]
    [ProducesResponseType(typeof(SpanPerformanceResult), StatusCodes.Status200OK)]
    public ActionResult<SpanPerformanceResult> GetPerformanceBenchmark()
    {
        _logger.LogInformation("Running performance benchmarks: Traditional vs Span");
        var result = _demoService.DemonstratePerformanceComparison();
        return Ok(result);
    }

    /// <summary>
    /// Get all span demonstrations at once
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public ActionResult<object> GetAllDemonstrations()
    {
        _logger.LogInformation("Demonstrating all C# 14 Span features");

        var results = new
        {
            implicitConversions = _demoService.DemonstrateImplicitConversions(),
            stringOperations = _demoService.DemonstrateStringOperations(),
            arrayOperations = _demoService.DemonstrateArrayOperations(),
            performance = _demoService.DemonstratePerformanceComparison()
        };

        return Ok(results);
    }

    /// <summary>
    /// Live example: Process product name with Span
    /// </summary>
    /// <param name="productName">Product name to process</param>
    /// <remarks>
    /// Shows real-world usage of Span for string processing:
    /// - Trimming whitespace (zero-copy)
    /// - Extracting substring (zero-copy)
    /// - Converting to uppercase
    /// Compares memory allocation with traditional approach
    /// </remarks>
    [HttpPost("process-product-name")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public ActionResult ProcessProductName([FromBody] string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            return BadRequest(new { error = "Product name cannot be empty" });
        }

        // Traditional approach (allocates new strings)
        var traditionalResult = productName.Trim().ToUpper();

        // Span approach (zero-copy until final ToString)
        ReadOnlySpan<char> spanResult = productName.AsSpan().Trim();
        
        return Ok(new
        {
            original = productName,
            traditional = new
            {
                result = traditionalResult,
                approach = "String methods (allocates memory for each operation)"
            },
            span = new
            {
                result = spanResult.ToString().ToUpper(),
                approach = "Span operations (zero-copy, allocates only for final result)"
            },
            performance = new
            {
                traditionalAllocations = "Multiple (Trim + ToUpper)",
                spanAllocations = "Single (final ToString + ToUpper)",
                benefit = "Reduced GC pressure and better performance"
            }
        });
    }
}
