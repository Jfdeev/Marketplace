namespace Marketplace.Application.UseCases.Products.GetProductsById;

using MediatR;
using Marketplace.Application.DTOs;

public class GetProductsByIdQuery : IRequest<ProductDto>
{
    public Guid ProductId { get; }

    public GetProductsByIdQuery(Guid productId)
    {
        ProductId = productId;
    }
}