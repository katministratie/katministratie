using System;

namespace Superkatten.Katministratie.Application.Configuration;

public sealed class ApiKeyConfigurationSection
{
    /// <summary>
    /// API key used during connection to the superkatten API
    /// </summary>
    public ApiKeyConfigType[] ApiKeys { get; set; } = Array.Empty<ApiKeyConfigType>();    
}
