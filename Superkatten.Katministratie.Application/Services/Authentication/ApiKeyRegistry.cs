using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Superkatten.Katministratie.Application.Configuration;
using Superkatten.Katministratie.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Superkatten.Katministratie.Application.Services.Authentication;

public class ApiKeyRegistry : IApiKeyRegistry
{
    private readonly ILogger<ApiKeyRegistry> _logger;
    private IReadOnlyCollection<ApiKey> _apiKeys = Array.Empty<ApiKey>();
    private IOptions<ApiKeyConfigurationSection> _apiKeyConfig;
    public ApiKeyRegistry(ILogger<ApiKeyRegistry> logger, IOptions<ApiKeyConfigurationSection> apiKeyConfig)
    {
        _logger = logger;
        _apiKeyConfig = apiKeyConfig;

        var apikeylist = _apiKeyConfig.Value.ApiKeys;
        _apiKeys = apikeylist
            .Select(ConvertToApiKey)
            .ToList()
            .AsReadOnly();
    }

    private ApiKey ConvertToApiKey(ApiKeyConfigType apiKeyConfigType, int index)
    {
        return new ApiKey
        {
            key = apiKeyConfigType.key,
            Roles = apiKeyConfigType.Roles
        };
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
