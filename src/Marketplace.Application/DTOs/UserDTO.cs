﻿namespace Marketplace.Application.DTOs;

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    string? Phone,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);