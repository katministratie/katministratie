using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Superkatten.Katministratie.ApiAuthentication.Services;

namespace Superkatten.Katministratie.ApiAuthentication;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddApiKeyAuthentication<TApiKeyValidator>(this AuthenticationBuilder builder)
        where TApiKeyValidator : class, IApiKeyValidator
    {
        builder.Services.AddTransient<IApiKeyValidator, TApiKeyValidator>();
        builder.AddScheme<ApiKeyOptions, ApiKeyHandler>(ApiKeyOptions.DEFAULT_SCHEME, cfg => { });

        return builder;
    }
}