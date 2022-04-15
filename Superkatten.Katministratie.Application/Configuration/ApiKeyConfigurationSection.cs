using Superkatten.Katministratie.Application.Models;
using System;
using System.Collections.Generic;

namespace Superkatten.Katministratie.Application.Configuration;

public sealed class ApiKeyConfigurationSection
{
    /// <summary>
    /// API key used during connection to the superkatten API
    /// </summary>
    public List<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();
}
