using Marketplace.Domain.Entities;

namespace Marketplace.Domain.Entities;

public class Cart
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation Properties
    public User User { get; private set; } = null!;
    public List<CartItem> Items { get; private set; } = new();

    private Cart() { } // EF Core

    public Cart(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        if (product.Stock < quantity)
            throw new InvalidOperationException("Insufficient stock");

        var existingItem = Items.FirstOrDefault(i => i.ProductId == product.Id);

        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            Items.Add(new CartItem(Id, product.Id, quantity, product.Price));
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveItem(Guid productId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            Items.Remove(item);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void UpdateItemQuantity(Guid productId, int quantity)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
            throw new InvalidOperationException("Item not found in cart");

        if (quantity <= 0)
        {
            RemoveItem(productId);
        }
        else
        {
            item.UpdateQuantity(quantity);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Clear()
    {
        Items.Clear();
        UpdatedAt = DateTime.UtcNow;
    }

    public decimal GetTotal()
    {
        return Items.Sum(i => i.GetTotal());
    }

    public int GetTotalItems()
    {
        return Items.Sum(i => i.Quantity);
    }
}