﻿namespace Marketplace.Domain.Exceptions;

public class InsufficientStockException : DomainException
{
    public InsufficientStockException(Guid productId, int requested, int available)
        : base($"Insufficient stock for product '{productId}'. Requested: {requested}, Available: {available}")
    {
    }
}