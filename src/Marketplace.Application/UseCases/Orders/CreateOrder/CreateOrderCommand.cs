using MediatR;
using Marketplace.Application.DTOs;

namespace Marketplace.Application.UseCases.Orders.CreateOrder;

public record CreateOrderCommand(Guid UserId) : IRequest<OrderDto>;