using Marketplace.Domain.Entities;
using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string? Phone { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation Properties
    public Cart? Cart { get; private set; }
    public List<Order> Orders { get; private set; } = new();

    private User() { } // EF Core

    public User(string name, Email email, string? phone = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("User name cannot be empty", nameof(name));

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Phone = phone;
        CreatedAt = DateTime.UtcNow;
        Cart = new Cart(Id);
    }

    public void UpdateProfile(string name, string? phone = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        Name = name;
        Phone = phone;
        UpdatedAt = DateTime.UtcNow;
    }
}