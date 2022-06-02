using Microsoft.AspNetCore.Http;
using Superkatten.Katministratie.Application.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Authenticate;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
    {
        var authorisationRequest = context.Request.Headers["Authorization"].FirstOrDefault();
        var token = authorisationRequest?.Split(" ").Last();

        var userId = jwtUtils.ValidateToken(token ?? string.Empty);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = userService.GetById(userId.Value);
        }

        await _next(context);
    }
}