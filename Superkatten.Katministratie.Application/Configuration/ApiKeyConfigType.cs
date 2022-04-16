using System;

namespace Superkatten.Katministratie.Application.Configuration;

public class ApiKeyConfigType
{
    public string key { get; init; } = string.Empty;
    public string[] Roles { get; init; } = Array.Empty<string>();
}
