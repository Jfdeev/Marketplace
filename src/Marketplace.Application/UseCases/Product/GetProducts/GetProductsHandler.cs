using MediatR;
using Marketplace.Application.DTOs;
using Marketplace.Domain.Repositories;

namespace Marketplace.Application.UseCases.Products.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = !string.IsNullOrWhiteSpace(request.SearchTerm)
            ? await _productRepository.SearchByNameAsync(request.SearchTerm, cancellationToken)
            : request.ActiveOnly
                ? await _productRepository.GetActiveAsync(cancellationToken)
                : await _productRepository.GetAllAsync(cancellationToken);

        return products.Select(p => new ProductDto(
            p.Id,
            p.Name,
            p.Description,
            p.Price.Amount,
            p.Price.Currency,
            p.Stock,
            p.IsActive,
            p.CreatedAt,
            p.UpdatedAt
        ));
    }
}
