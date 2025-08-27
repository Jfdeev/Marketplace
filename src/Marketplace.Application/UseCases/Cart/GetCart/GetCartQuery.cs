using MediatR;
using Marketplace.Application.DTOs;

namespace Marketplace.Application.UseCases.Cart.GetCart;

public class GetCartQuery : IRequest<CartDto>
{
    public Guid UserId { get; }

    public GetCartQuery(Guid userId)
    {
        UserId = userId;
    }
}