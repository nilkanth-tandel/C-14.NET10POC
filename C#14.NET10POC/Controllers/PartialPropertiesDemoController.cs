using C_14.NET10POC.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace C_14.NET10POC.Controllers;

/// <summary>
/// Controller demonstrating C# 14 partial properties
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PartialPropertiesDemoController : ControllerBase
{
    private readonly IPartialPropertiesService _demoService;
    private readonly ILogger<PartialPropertiesDemoController> _logger;

    public PartialPropertiesDemoController(
        IPartialPropertiesService demoService,
        ILogger<PartialPropertiesDemoController> logger)
    {
        _demoService = demoService;
        _logger = logger;
    }

    /// <summary>
    /// Demonstrate C# 14 partial properties
    /// </summary>
    /// <remarks>
    /// Shows:
    /// - Property declaration in one partial class
    /// - Property implementation in another partial class
    /// - Validation and transformation in property setters
    /// </remarks>
    [HttpGet("partial-properties")]
    [ProducesResponseType(typeof(PartialPropertiesResult), StatusCodes.Status200OK)]
    public ActionResult<PartialPropertiesResult> GetPartialProperties()
    {
        _logger.LogInformation("Demonstrating C# 14 partial properties");
        var result = _demoService.DemonstratePartialProperties();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate partial methods (existing feature)
    /// </summary>
    [HttpGet("partial-methods")]
    [ProducesResponseType(typeof(PartialPropertiesResult), StatusCodes.Status200OK)]
    public ActionResult<PartialPropertiesResult> GetPartialMethods()
    {
        _logger.LogInformation("Demonstrating partial methods");
        var result = _demoService.DemonstratePartialMethods();
        return Ok(result);
    }

    /// <summary>
    /// Get all demonstrations
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public ActionResult<object> GetAllDemonstrations()
    {
        _logger.LogInformation("Demonstrating all partial features");

        var results = new
        {
            partialProperties = _demoService.DemonstratePartialProperties(),
            partialMethods = _demoService.DemonstratePartialMethods()
        };

        return Ok(results);
    }
}
