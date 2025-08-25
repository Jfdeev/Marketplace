using Marketplace.Domain.Entities;

namespace Marketplace.Application.DTOs;

public record OrderDto(
    Guid Id,
    Guid UserId,
    List<OrderItemDto> Items,
    decimal Total,
    string Currency,
    OrderStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record OrderItemDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Total
);