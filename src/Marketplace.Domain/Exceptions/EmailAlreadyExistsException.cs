using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Exceptions;

public class EmailAlreadyExistsException : DomainException
{
    public EmailAlreadyExistsException(Email email)
        : base($"Email '{email}' is already registered.")
    {
    }
}