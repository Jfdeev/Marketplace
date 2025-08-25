using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Money Total { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation Properties
    public User User { get; private set; } = null!;
    public List<OrderItem> Items { get; private set; } = new();

    private Order() { } // EF Core

    public Order(Guid userId, List<CartItem> cartItems)
    {
        if (!cartItems.Any())
            throw new ArgumentException("Order must have at least one item");

        Id = Guid.NewGuid();
        UserId = userId;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;

        foreach (var cartItem in cartItems)
        {
            Items.Add(new OrderItem(Id, cartItem.ProductId, cartItem.Quantity, cartItem.UnitPrice));
        }

        Total = new Money(Items.Sum(i => i.GetTotal()));
    }

    public void ConfirmPayment()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Only pending orders can be confirmed");

        Status = OrderStatus.Confirmed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot cancel delivered or already cancelled orders");

        Status = OrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.Confirmed)
            throw new InvalidOperationException("Only confirmed orders can be delivered");

        Status = OrderStatus.Delivered;
        UpdatedAt = DateTime.UtcNow;
    }
}