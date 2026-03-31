using C_14.NET10POC.Application.DTOs;
using C_14.NET10POC.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace C_14.NET10POC.Controllers;

/// <summary>
/// API Controller for Category management
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        _logger.LogInformation("Getting all categories");
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Get category by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        _logger.LogInformation("Getting category {CategoryId}", id);
        var category = await _categoryService.GetCategoryByIdAsync(id);
        
        if (category == null)
        {
            _logger.LogWarning("Category {CategoryId} not found", id);
            return NotFound();
        }
        
        return Ok(category);
    }

    /// <summary>
    /// Create new category
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto dto)
    {
        _logger.LogInformation("Creating category: {CategoryName}", dto.Name);
        var category = await _categoryService.CreateCategoryAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    /// <summary>
    /// Update existing category
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] UpdateCategoryDto dto)
    {
        _logger.LogInformation("Updating category {CategoryId}", id);
        var category = await _categoryService.UpdateCategoryAsync(id, dto);
        
        if (category == null)
        {
            _logger.LogWarning("Category {CategoryId} not found for update", id);
            return NotFound();
        }
        
        return Ok(category);
    }

    /// <summary>
    /// Delete category
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting category {CategoryId}", id);
        var success = await _categoryService.DeleteCategoryAsync(id);
        
        if (!success)
        {
            _logger.LogWarning("Category {CategoryId} not found for deletion", id);
            return NotFound();
        }
        
        return NoContent();
    }
}
