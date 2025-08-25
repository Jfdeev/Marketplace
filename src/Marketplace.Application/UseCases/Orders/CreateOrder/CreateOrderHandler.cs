using MediatR;
using Marketplace.Application.DTOs;
using Marketplace.Application.Interfaces;
using Marketplace.Domain.Entities;
using Marketplace.Domain.Exceptions;
using Marketplace.Domain.Repositories;

namespace Marketplace.Application.UseCases.Orders.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new CartNotFoundException(request.UserId);

        if (!cart.Items.Any())
            throw new InvalidOperationException("Cannot create order from empty cart");

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            // Verificar estoque e reduzir
            foreach (var item in cart.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken)
                    ?? throw new ProductNotFoundException(item.ProductId);

                if (product.Stock < item.Quantity)
                    throw new InsufficientStockException(item.ProductId, item.Quantity, product.Stock);

                product.ReduceStock(item.Quantity);
                await _productRepository.UpdateAsync(product, cancellationToken);
            }

            // Criar pedido
            var order = new Order(request.UserId, cart.Items.ToList());
            await _orderRepository.AddAsync(order, cancellationToken);

            // Limpar carrinho
            cart.Clear();
            await _cartRepository.UpdateAsync(cart, cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return new OrderDto(
                order.Id,
                order.UserId,
                order.Items.Select(i => new OrderItemDto(
                    i.Id,
                    i.ProductId,
                    i.Product.Name,
                    i.Quantity,
                    i.UnitPrice.Amount,
                    i.GetTotal()
                )).ToList(),
                order.Total.Amount,
                order.Total.Currency,
                order.Status,
                order.CreatedAt,
                order.UpdatedAt
            );
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}