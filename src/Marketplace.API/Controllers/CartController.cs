using MediatR;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Application.UseCases.Cart.AddToCart;
using Marketplace.Application.UseCases.Cart.GetCart;

namespace Marketplace.API.Controllers;

[ApiController]
[Route("api/users/{userId:guid}/[controller]")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddToCart(
        Guid userId,
        [FromBody] AddToCartRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new AddToCartCommand(userId, request.ProductId, request.Quantity);
        var cart = await _mediator.Send(command, cancellationToken);
        return Ok(cart);
    }

    [HttpGet]
    public async Task<IActionResult> GetCart(Guid userId, CancellationToken cancellationToken = default)
    {
        var query = new GetCartQuery(userId);
        var cart = await _mediator.Send(query, cancellationToken);
        return Ok(cart);
    }

public record AddToCartRequest(Guid ProductId, int Quantity);
}