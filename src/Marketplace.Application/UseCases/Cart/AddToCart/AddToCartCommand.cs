using MediatR;
using Marketplace.Application.DTOs;

namespace Marketplace.Application.UseCases.Cart.AddToCart;

public record AddToCartCommand(
    Guid UserId,
    Guid ProductId,
    int Quantity
) : IRequest<CartDto>;