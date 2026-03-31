using C_14.NET10POC.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace C_14.NET10POC.Controllers;

/// <summary>
/// Controller demonstrating C# 14 Extension Members
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ExtensionDemoController : ControllerBase
{
    private readonly IExtensionDemoService _demoService;
    private readonly ILogger<ExtensionDemoController> _logger;

    public ExtensionDemoController(
        IExtensionDemoService demoService, 
        ILogger<ExtensionDemoController> logger)
    {
        _demoService = demoService;
        _logger = logger;
    }

    /// <summary>
    /// Demonstrate C# 14 Extension Properties and Methods on Product collections
    /// </summary>
    /// <remarks>
    /// Shows:
    /// - Extension properties (HasInStockItems, TotalInventoryValue, IsEmpty, InStockCount)
    /// - Extension methods (WhereInStock, WherePriceRange, WhereLowStock, CalculateTotalValue)
    /// </remarks>
    [HttpGet("product-extensions")]
    [ProducesResponseType(typeof(ExtensionDemoResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<ExtensionDemoResult>> GetProductExtensions()
    {
        _logger.LogInformation("Demonstrating C# 14 Product Extension Members");
        var result = await _demoService.DemonstrateProductExtensions();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate C# 14 Extension Properties and Methods on Category collections
    /// </summary>
    /// <remarks>
    /// Shows:
    /// - Extension properties (HasActiveCategories, ActiveCount, IsEmpty)
    /// - Extension methods (WhereActive, WithProducts, ToCategoryList)
    /// </remarks>
    [HttpGet("category-extensions")]
    [ProducesResponseType(typeof(ExtensionDemoResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<ExtensionDemoResult>> GetCategoryExtensions()
    {
        _logger.LogInformation("Demonstrating C# 14 Category Extension Members");
        var result = await _demoService.DemonstrateCategoryExtensions();
        return Ok(result);
    }

    /// <summary>
    /// Demonstrate C# 14 Static Extension Members and Operators
    /// </summary>
    /// <remarks>
    /// Shows:
    /// - Static extension properties (Empty)
    /// - Static extension methods (Combine, CreateSampleCollection)
    /// - Extension operators (+ operator for concatenation)
    /// </remarks>
    [HttpGet("static-extensions")]
    [ProducesResponseType(typeof(ExtensionDemoResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<ExtensionDemoResult>> GetStaticExtensions()
    {
        _logger.LogInformation("Demonstrating C# 14 Static Extension Members");
        var result = await _demoService.DemonstrateStaticExtensions();
        return Ok(result);
    }

    /// <summary>
    /// Get all extension demonstrations at once
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<ExtensionDemoResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ExtensionDemoResult>>> GetAllDemonstrations()
    {
        _logger.LogInformation("Demonstrating all C# 14 Extension Members");
        
        var results = new List<ExtensionDemoResult>
        {
            await _demoService.DemonstrateProductExtensions(),
            await _demoService.DemonstrateCategoryExtensions(),
            await _demoService.DemonstrateStaticExtensions()
        };

        return Ok(results);
    }
}
