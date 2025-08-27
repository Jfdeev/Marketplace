using MediatR;
using Marketplace.Application.DTOs;
using Marketplace.Domain.Repositories;

namespace Marketplace.Application.UseCases.Products.GetProductsById;

public class GetProductsByIdHandler : IRequestHandler<GetProductsByIdQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null)
            return null;

        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price.Amount,
            product.Price.Currency,
            product.Stock,
            product.IsActive,
            product.CreatedAt,
            product.UpdatedAt
        );
    }
}
