using Microsoft.AspNetCore.Authentication;

namespace Superkatten.Katministratie.Application.Services.Authentication
{
    public class ApiKeyOptions : AuthenticationSchemeOptions
    {
        public const string DEFAULT_SCHEME = "ApiKeyAuth";
        public const string DEFAULT_HEADER = "X-Api-key";

        public string Scheme { get; set; } = DEFAULT_SCHEME;
        public string AuthenticationType { get; set; } = DEFAULT_SCHEME;
        public string ApiKeyHeader { get; set; } = DEFAULT_HEADER;
    }
}
