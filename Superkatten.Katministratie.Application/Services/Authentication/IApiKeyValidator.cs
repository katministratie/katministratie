using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services.Authentication
{
    public interface IApiKeyValidator
    {
        Task<ApiKeyResult> IsApiKeyValidAsync(string apiKey);
    }
}
