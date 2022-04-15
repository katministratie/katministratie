using System;

namespace Superkatten.Katministratie.Application.Configuration;

public sealed class ApiKeyConfigurationSection
{
    //TODO:  For test purposes only, please remove
    public string Test { get; set; } = String.Empty;

    /// <summary>
    /// API key used during connection to the superkatten API
    /// </summary>
    public ApiKeyConfigType[] ApiKeys { get; set; } = Array.Empty<ApiKeyConfigType>();
    
}
