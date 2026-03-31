using C_14.NET10POC.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace C_14.NET10POC.Controllers;

/// <summary>
/// Controller demonstrating C# 14 nameof with unbound generic types
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class NameofDemoController : ControllerBase
{
    private readonly INameofDemoService _demoService;
    private readonly ILogger<NameofDemoController> _logger;

    public NameofDemoController(
        INameofDemoService demoService,
        ILogger<NameofDemoController> logger)
    {
        _demoService = demoService;
        _logger = logger;
    }

    /// <summary>
    /// Demonstrate C# 14 nameof with unbound generic types
    /// </summary>
    /// <remarks>
    /// Shows how to use nameof with generic types without type arguments:
    /// - nameof(List&lt;&gt;) returns "List" (not "List`1")
    /// - nameof(Dictionary&lt;,&gt;) returns "Dictionary" (not "Dictionary`2")
    /// - Much cleaner than typeof().Name
    /// </remarks>
    [HttpGet("unbound-generics")]
    [ProducesResponseType(typeof(NameofDemoResult), StatusCodes.Status200OK)]
    public ActionResult<NameofDemoResult> GetUnboundGenerics()
    {
        _logger.LogInformation("Demonstrating C# 14 nameof with unbound generic types");
        var result = _demoService.DemonstrateUnboundGenerics();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate nameof in logging scenarios
    /// </summary>
    /// <remarks>
    /// Shows practical use cases:
    /// - Repository initialization logging
    /// - Cache key generation
    /// - Collection type logging
    /// - Generic method call logging
    /// </remarks>
    [HttpGet("logging-scenarios")]
    [ProducesResponseType(typeof(NameofDemoResult), StatusCodes.Status200OK)]
    public ActionResult<NameofDemoResult> GetLoggingScenarios()
    {
        _logger.LogInformation("Demonstrating C# 14 nameof in logging scenarios");
        var result = _demoService.DemonstrateLoggingScenarios();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate nameof in error messages
    /// </summary>
    /// <remarks>
    /// Shows error message scenarios:
    /// - Validation errors
    /// - Type mismatch errors
    /// - Null reference errors
    /// - Configuration errors
    /// - Serialization errors
    /// </remarks>
    [HttpGet("error-messages")]
    [ProducesResponseType(typeof(NameofDemoResult), StatusCodes.Status200OK)]
    public ActionResult<NameofDemoResult> GetErrorMessages()
    {
        _logger.LogInformation("Demonstrating C# 14 nameof in error messages");
        var result = _demoService.DemonstrateErrorMessages();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate type comparison with nameof
    /// </summary>
    /// <remarks>
    /// Compares:
    /// - nameof(List&lt;&gt;) returns "List"
    /// - typeof(List&lt;&gt;).Name returns "List`1" (with backtick)
    /// Shows the improvement in C# 14
    /// </remarks>
    [HttpGet("type-comparison")]
    [ProducesResponseType(typeof(NameofDemoResult), StatusCodes.Status200OK)]
    public ActionResult<NameofDemoResult> GetTypeComparison()
    {
        _logger.LogInformation("Demonstrating C# 14 type name comparison");
        var result = _demoService.DemonstrateTypeComparison();
        return Ok(result);
    }

    /// <summary>
    /// Get all nameof demonstrations at once
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<NameofDemoResult>), StatusCodes.Status200OK)]
    public ActionResult<List<NameofDemoResult>> GetAllDemonstrations()
    {
        _logger.LogInformation("Demonstrating all C# 14 nameof features");

        var results = new List<NameofDemoResult>
        {
            _demoService.DemonstrateUnboundGenerics(),
            _demoService.DemonstrateLoggingScenarios(),
            _demoService.DemonstrateErrorMessages(),
            _demoService.DemonstrateTypeComparison()
        };

        return Ok(results);
    }

    /// <summary>
    /// Live example: Log a message using nameof with generics
    /// </summary>
    /// <remarks>
    /// This endpoint logs a message using C# 14 nameof feature
    /// Check your application logs to see the clean type names!
    /// </remarks>
    [HttpPost("log-example")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public ActionResult LogExample([FromBody] string? customMessage = null)
    {
        var message = customMessage ?? "Testing C# 14 nameof feature";

        // C# 14: Clean logging with unbound generic types
        _logger.LogInformation(
            "{Message} - Using {CollectionType} for {DictionaryType} of {EntityType}",
            message,
            nameof(List<>),          // Returns "List" (not "List`1")
            nameof(Dictionary<,>),   // Returns "Dictionary" (not "Dictionary`2")
            nameof(Domain.Entities.Product) // Returns "Product"
        );

        return Ok(new
        {
            logged = true,
            message,
            typesUsed = new
            {
                list = nameof(List<>),
                dictionary = nameof(Dictionary<,>),
                product = nameof(Domain.Entities.Product)
            },
            comparison = new
            {
                oldWay = typeof(List<>).Name,  // "List`1"
                newWay = nameof(List<>)        // "List"
            }
        });
    }
}
