using Superkatten.Katministratie.Application.Models;
using System.Collections.Generic;

namespace Superkatten.Katministratie.Application.Services.Authentication;

public interface IApiKeyRegistry
{
    IReadOnlyCollection<ApiKey> GetApiKeys();
    void SetApiKeys(IReadOnlyCollection<ApiKey> apiKeys);
}
