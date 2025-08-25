namespace Marketplace.Application.DTOs;

public record CartDto(
    Guid Id,
    Guid UserId,
    List<CartItemDto> Items,
    decimal Total,
    int TotalItems,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CartItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice
);