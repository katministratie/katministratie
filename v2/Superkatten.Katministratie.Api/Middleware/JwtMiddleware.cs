using Superkatten.Katministratie.Application.Contracts.Interfaces;
using Superkatten.Katministratie.Domain;
using Superkatten.Katministratie.Domain.Shared;

namespace Superkatten.Katministratie.HttpApi.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    // TODO: Get user from JWT validation

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
    {
        var authorisationKey = context.Request.Headers["Authorization"].FirstOrDefault();
        var token = authorisationKey?.Split(" ").Last();

        //var userId = jwtUtils.ValidateToken(token ?? string.Empty);
        //if (userId is not null)
        //{
        //    // attach user to context on successful jwt validation
        //}

        // TODO: Tijdelijk uitgeschakeld
        //if (token is not null)
        //{
            context.Items["user"] = new User("Johan", "123")
            {
                Permissions = new List<UserPermissions>() {
                    UserPermissions.Administrator
                }
            };
        //}

        await _next(context);
    }

}