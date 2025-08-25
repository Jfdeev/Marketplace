using MediatR;
using Marketplace.Application.DTOs;
using Marketplace.Application.Interfaces;
using Marketplace.Domain.Exceptions;
using Marketplace.Domain.Repositories;

namespace Marketplace.Application.UseCases.Cart.AddToCart;

public class AddToCartHandler : IRequestHandler<AddToCartCommand, CartDto>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddToCartHandler(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CartDto> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new ProductNotFoundException(request.ProductId);

        if (product.Stock < request.Quantity)
            throw new InsufficientStockException(request.ProductId, request.Quantity, product.Stock);

        var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new CartNotFoundException(request.UserId);

        cart.AddItem(product, request.Quantity);

        await _cartRepository.UpdateAsync(cart, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CartDto(
            cart.Id,
            cart.UserId,
            cart.Items.Select(i => new CartItemDto(
                i.Id,
                i.ProductId,
                i.Product.Name,
                i.Quantity,
                i.UnitPrice.Amount,
                i.GetTotal()
            )).ToList(),
            cart.GetTotal(),
            cart.GetTotalItems(),
            cart.CreatedAt,
            cart.UpdatedAt
        );
    }
}