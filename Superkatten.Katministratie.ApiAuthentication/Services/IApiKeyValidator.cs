using System.Threading.Tasks;

namespace Superkatten.Katministratie.ApiAuthentication.Services
{
    public interface IApiKeyValidator
    {
        Task<ApiKeyResult> IsApiKeyValidAsync(string apiKey);
    }
}
