namespace C_14.NET10POC.Application.DTOs;

public record ProductDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    int CategoryId,
    string CategoryName,
    bool IsActive,
    bool IsInStock,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    int CategoryId
);

public record UpdateProductDto(
    string? Name,
    string? Description,
    decimal? Price,
    int? StockQuantity,
    int? CategoryId,
    bool? IsActive
);
