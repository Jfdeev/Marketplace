using MediatR;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Application.UseCases.Products.CreateProduct;
using Marketplace.Application.UseCases.Products.GetProducts;
using Marketplace.Application.UseCases.Products.GetProductsById;

namespace Marketplace.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] bool activateOnly = true, [FromQuery] string? searchTerm = null, CancellationToken cancellationToken = default)
    {
        var query = new GetProductsQuery(activateOnly, searchTerm);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken = default)
    {
        var product = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetProductsByIdQuery(id);
        var product = await _mediator.Send(query, cancellationToken);
        if (product == null)
            return NotFound();

        return Ok(product);
    }
}
