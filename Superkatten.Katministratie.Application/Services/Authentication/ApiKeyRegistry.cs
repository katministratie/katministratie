using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Models;
using System;
using System.Collections.Generic;

namespace Superkatten.Katministratie.Application.Services.Authentication;

public class ApiKeyRegistry : IApiKeyRegistry
{
    private readonly ILogger<ApiKeyRegistry> _logger;
    private IReadOnlyCollection<ApiKey> _apiKeys = Array.Empty<ApiKey>();
    public ApiKeyRegistry(ILogger<ApiKeyRegistry> logger)
    {
        _logger = logger;
    }

    public IReadOnlyCollection<ApiKey> GetApiKeys()
    {
        if (_apiKeys.Count == 0)
        {
            _logger.LogWarning("The api key list is empty.");
        }
        return _apiKeys;
    }

    public void SetApiKeys(IReadOnlyCollection<ApiKey> apiKeys)
    {
        apiKeys = apiKeys ?? throw new ArgumentNullException(nameof(apiKeys));
        var newApiKeys = new List<ApiKey>();
        foreach (var apiKey in apiKeys)
        {
            if (string.IsNullOrEmpty(apiKey.key))
            {
                _logger.LogError("An API key with an empty key property was provided, this is not allowed and was ignored.");
                continue;
            }

            newApiKeys.Add(apiKey);
        }
        _apiKeys = newApiKeys.AsReadOnly();
    }
}
