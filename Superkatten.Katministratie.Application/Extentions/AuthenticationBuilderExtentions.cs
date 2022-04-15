using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Superkatten.Katministratie.Application.Services.Authentication;

namespace Superkatten.Katministratie.Application.Extentions;

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