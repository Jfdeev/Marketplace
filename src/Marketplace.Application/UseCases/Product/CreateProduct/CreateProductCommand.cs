using MediatR;
using Marketplace.Application.DTOs;

namespace Marketplace.Application.UseCases.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int Stock,
    string? ImageUrl = null
) : IRequest<ProductDto>;