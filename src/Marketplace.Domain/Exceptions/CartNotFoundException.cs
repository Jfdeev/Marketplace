namespace Marketplace.Domain.Exceptions;

public class CartNotFoundException : DomainException
{
    public CartNotFoundException(Guid userId)
        : base($"Cart for user '{userId}' was not found.")
    {
    }
}