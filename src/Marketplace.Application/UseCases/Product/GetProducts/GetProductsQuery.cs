using MediatR;
using Marketplace.Application.DTOs;

namespace Marketplace.Application.UseCases.Products.GetProducts;

public record GetProductsQuery(
    bool ActiveOnly = true,
    string? SearchTerm = null
) : IRequest<IEnumerable<ProductDto>>;