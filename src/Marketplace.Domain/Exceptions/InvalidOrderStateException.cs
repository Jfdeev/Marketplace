using Marketplace.Domain.Entities;

namespace Marketplace.Domain.Exceptions;

public class InvalidOrderStateException : DomainException
{
    public InvalidOrderStateException(OrderStatus currentStatus, OrderStatus attemptedStatus)
        : base($"Cannot change order status from '{currentStatus}' to '{attemptedStatus}'.")
    {
    }
}