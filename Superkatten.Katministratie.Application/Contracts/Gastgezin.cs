﻿namespace Superkatten.Katministratie.Application.Contracts
{
    public class Gastgezin
    {
        public string Name { get; init; } = string.Empty;
        public string? Address { get; init; } = string.Empty;
        public string? City { get; init; } = string.Empty;
        public string? Phone { get; init; } = string.Empty;
    }
}
