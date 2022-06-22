﻿using Superkatten.Katministratie.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Superkatten.Katministratie.Domain.Entities;

public class User
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public bool IsEnabled { get; init; } = true;  //TODO: default to false + way to enable ?
    public IReadOnlyCollection<PermissionEnum> Permissions { get; init; } = Array.Empty<PermissionEnum>();

    [JsonIgnore]
    public string? PasswordHash { get; set; }

    public User()
    {
        Permissions = new List<PermissionEnum>() 
        { 
            PermissionEnum.ViewOnly 
        };
    }

    public User Update(string username, string email, string name, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new DomainException($"username can not be empty");
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new DomainException($"password can not be empty");
        }

        return new User
        {
            Id = Id,
            IsEnabled = IsEnabled,
            Permissions = Permissions,

            Name = name,
            Email = email,
            Username = username,
            PasswordHash = passwordHash
        };
    }
}
