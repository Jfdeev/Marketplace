using MediatR;
using Marketplace.Application.DTOs;
using Marketplace.Domain.Repositories;

namespace Marketplace.Application.UseCases.Cart.GetCart;

public class GetCartHandler : IRequestHandler<GetCartQuery, CartDto>
{
    private readonly ICartRepository _cartRepository;

    public GetCartHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (cart == null)
        {
            return new CartDto(
                Guid.Empty,
                request.UserId,
                new List<CartItemDto>(),
                0,
                0,
                DateTime.UtcNow,
                null
            );
        }

        var items = cart.Items.Select(item => new CartItemDto(
            item.ProductId,
            item.Product.Name,
            item.UnitPrice.Amount,
            item.Quantity,
            (item.UnitPrice * item.Quantity).Amount
        )).ToList();

        return new CartDto(
            cart.Id,
            cart.UserId,
            items,
            items.Sum(i => i.TotalPrice),
            items.Sum(i => i.Quantity),
            cart.CreatedAt,
            cart.UpdatedAt
        );
    }
}