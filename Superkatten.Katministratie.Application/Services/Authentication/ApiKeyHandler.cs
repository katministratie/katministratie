using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services.Authentication
{
    internal class ApiKeyHandler : AuthenticationHandler<ApiKeyOptions>
    {
        private readonly IApiKeyValidator _apiKeyValidator;

        public ApiKeyHandler(
            IOptionsMonitor<ApiKeyOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IApiKeyValidator apiKeyValidator
            ) : base(options, logger, encoder, clock)
        {
            _apiKeyValidator = apiKeyValidator;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(Options.ApiKeyHeader, out var apiKeyHeader))
            {
                return AuthenticateResult.NoResult();
            }

            var apiKeyResult = await _apiKeyValidator.IsApiKeyValidAsync(apiKeyHeader);
            if (!apiKeyResult.IsSuccess)
            {
                return AuthenticateResult.Fail("Invalid API key");
            }

            var identity = new ClaimsIdentity(apiKeyResult.Claims, Options.AuthenticationType);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Options.Scheme);

            return AuthenticateResult.Success(ticket);
        }

        protected async override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.ContentType = "application/json";
            /*var challenge = new ProblemDetails
            {
                Title = nameof(HttpStatusCode.Unauthorized),
                Detail = Request.Headers.ContainsKey(Options.ApiKeyHeader)
                    ? $"The {Options.ApiKeyHeader} does not contain a valid API key"
                    : $"The {Options.ApiKeyHeader} header is missing from the request",
                Status = Response.StatusCode
            };
            */

            await Response.Body.WriteAsync(JsonSerializer.SerializeToUtf8Bytes("API key not valid"));
        }

        protected async override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            /*Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.ContentType = "application/json";
            var forbidden = new ProblemDetails
            {
                Title = nameof(HttpStatusCode.Forbidden),
                Detail = "Your API key does not grand you permission to this part of the application",
                Status = Response.StatusCode
            };*/

            await Request.Body.WriteAsync(JsonSerializer.SerializeToUtf8Bytes("Forbidden to access this part"));
        }
    }
}
