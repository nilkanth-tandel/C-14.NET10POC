namespace C_14.NET10POC.Application.DTOs;

public record CategoryDto(
    int Id,
    string Name,
    string Description,
    bool IsActive,
    int ProductCount,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateCategoryDto(
    string Name,
    string Description
);

public record UpdateCategoryDto(
    string? Name,
    string? Description,
    bool? IsActive
);
