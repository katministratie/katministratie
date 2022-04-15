using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services.Authentication;

public class ApiKeyValidator : IApiKeyValidator
{
    private readonly IApiKeyRegistry _apiKeyRegistry;

    public ApiKeyValidator(IApiKeyRegistry apiKeyRegistry)
    {
        _apiKeyRegistry = apiKeyRegistry;
    }

    public Task<ApiKeyResult> IsApiKeyValidAsync(string apiKey)
    {
        if(string.IsNullOrEmpty(apiKey))
        {
            return Task.FromResult(ApiKeyResult.Fail());
        }

        var apiKeys = _apiKeyRegistry.GetApiKeys();
        var matchingApiKey = apiKeys.FirstOrDefault(key => key.key == apiKey);
        return matchingApiKey is null
            ? Task.FromResult(ApiKeyResult.Fail())
            : Task.FromResult(ApiKeyResult.Success(ToClaims(matchingApiKey.Roles)));
    }

    private IEnumerable<Claim> ToClaims(IReadOnlyCollection<string> roles)
    {
        return roles.Select(role => new Claim(ClaimTypes.Role, role));
    }
}

public class ApiKeyResult
{
    internal static ApiKeyResult Fail()
    {
        throw new NotImplementedException();
    }

    internal static ApiKeyResult Success(IEnumerable<Claim> enumerable)
    {
        throw new NotImplementedException();
    }
}

public interface IApiKeyValidator
{
}