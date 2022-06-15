using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Authorization;

internal class AuthorisationHandler : IAuthorizationHandler
{
    private readonly IUserAuthorisationRepository _userAuthorisationRepository;

    public AuthorisationHandler(IUserAuthorisationRepository userAuthorisationRepository)
    {
        _userAuthorisationRepository = userAuthorisationRepository;
    }

    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        _ = int.TryParse(context.User.Claims.First(c => c.Type == "id").Value, out var userId);
        var user = _userAuthorisationRepository
            .GetAllUsers()
            .FirstOrDefault(u => u.Id == userId);

        if (user is null)
        {
            return Task.CompletedTask;
        }

        foreach(var requirement in context.Requirements)
        {

            if (requirement.GetType() == typeof(RolesAuthorizationRequirement))
            {
                var permissions = user.Permissions.Select(s => s.ToString()).ToList();
                var rolesRequirement = (RolesAuthorizationRequirement)requirement;
                var permissionCount = rolesRequirement.AllowedRoles.Intersect(permissions).ToList().Count;

                if (permissionCount > 0)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
        }

        context.Fail(
            new AuthorizationFailureReason(this, "User is not authorized to use this function")
        );

        return Task.CompletedTask;
    }

    private bool CheckUserPermissions(IReadOnlyCollection<PermissionEnum> permissions, string allowedPermission)
    {
        foreach (var permission in permissions)
        {
            var permissionAsString = permission.ToString();
            if (permissionAsString.Equals(allowedPermission))
            {
                return true;
            }    
        }

        return false;
    }
}
