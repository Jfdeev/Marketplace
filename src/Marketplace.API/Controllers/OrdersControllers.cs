using MediatR;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Application.UseCases.Orders.CreateOrder;

namespace Marketplace.API.Controllers;

[ApiController]
[Route("api/users/{userId:guid}/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateOrderCommand(userId);
        var order = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetOrderById), new { userId, id = order.Id }, order);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserOrders(Guid userId)
    {
        //Implementar GetUserOrdersQuery
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(Guid userId, Guid id)
    {
        // Implementar GetOrderByIdQuery
        return Ok();
    }
}