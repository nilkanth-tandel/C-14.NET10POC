using C_14.NET10POC.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace C_14.NET10POC.Controllers;

/// <summary>
/// Controller demonstrating operator concepts
/// (Compound assignment operators not yet available)
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CompoundOperatorDemoController : ControllerBase
{
    private readonly ICompoundOperatorService _demoService;
    private readonly ILogger<CompoundOperatorDemoController> _logger;

    public CompoundOperatorDemoController(
        ICompoundOperatorService demoService,
        ILogger<CompoundOperatorDemoController> logger)
    {
        _demoService = demoService;
        _logger = logger;
    }

    /// <summary>
    /// Demonstrate compound assignment operator concepts
    /// </summary>
    /// <remarks>
    /// Shows what user-defined compound assignment operators would enable (when available):
    /// - Custom += operator
    /// - Custom -= operator
    /// - Custom *= operator
    /// 
    /// Currently shows workarounds using regular operators
    /// </remarks>
    [HttpGet("compound-assignment")]
    [ProducesResponseType(typeof(CompoundOperatorResult), StatusCodes.Status200OK)]
    public ActionResult<CompoundOperatorResult> GetCompoundAssignment()
    {
        _logger.LogInformation("Demonstrating compound assignment concepts");
        var result = _demoService.DemonstrateCompoundAssignment();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate plus-equals operator
    /// </summary>
    [HttpGet("plus-equals")]
    [ProducesResponseType(typeof(CompoundOperatorResult), StatusCodes.Status200OK)]
    public ActionResult<CompoundOperatorResult> GetPlusEquals()
    {
        _logger.LogInformation("Demonstrating plus-equals operator");
        var result = _demoService.DemonstratePlusEquals();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate minus-equals operator
    /// </summary>
    [HttpGet("minus-equals")]
    [ProducesResponseType(typeof(CompoundOperatorResult), StatusCodes.Status200OK)]
    public ActionResult<CompoundOperatorResult> GetMinusEquals()
    {
        _logger.LogInformation("Demonstrating minus-equals operator");
        var result = _demoService.DemonstrateMinusEquals();
        return Ok(result);
    }

    /// <summary>
    /// Get all demonstrations
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public ActionResult<object> GetAllDemonstrations()
    {
        _logger.LogInformation("Demonstrating all compound operator concepts");

        var results = new
        {
            compoundAssignment = _demoService.DemonstrateCompoundAssignment(),
            plusEquals = _demoService.DemonstratePlusEquals(),
            minusEquals = _demoService.DemonstrateMinusEquals()
        };

        return Ok(results);
    }
}
