using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Superkatten.Katministratie.Infrastructure.Services;

namespace Superkatten.Katministratie.Application.Authorisation;

internal class AuthorisationHandler : IAuthorizationHandler
{
    private readonly IUserRepository _userAuthorisationRepository;

    public AuthorisationHandler(IUserRepository userAuthorisationRepository)
    {
        _userAuthorisationRepository = userAuthorisationRepository;
    }

    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        var userName = string.Empty;

        var claims = context.User.Claims;

        if (!claims.Any())
        {
            // TODO: Tijdelijk uitgeschakeld
            //context.Fail(new AuthorizationFailureReason(this, "No user found in list."));
            context.Succeed(new RolesAuthorizationRequirement(new List<string> { "Administrator" }));
            return Task.CompletedTask;
        }

        userName = claims.First(c => c.Type == "UserName").Value;

        var user = _userAuthorisationRepository
            .GetAllUsers()
            .FirstOrDefault(u => u.Username == userName);

        if (user is null)
        {
            context.Fail(new AuthorizationFailureReason(this, "No user logged in."));
            return Task.CompletedTask;
        }

        foreach (var requirement in context.Requirements)
        {

            if (requirement.GetType() == typeof(RolesAuthorizationRequirement))
            {
                var permissions = user.Permissions.Select(s => s.ToString()).ToList();
                var rolesRequirement = (RolesAuthorizationRequirement)requirement;

                var hasPermission = rolesRequirement
                    .AllowedRoles
                    .Intersect(permissions)
                    .Any();

                if (hasPermission)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
        }

        var failException = new AuthorizationFailureReason(this, "User is not authorized to use this function");
        context.Fail(failException);
        return Task.CompletedTask;
    }
}
