using Microsoft.AspNetCore.Authorization;
using Superkatten.Katministratie.Domain.Shared;

namespace Superkatten.Katministratie.Application.Authorisation;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params UserPermissions[] permissions) : base()
    {
        Roles = string.Join(",", permissions);
    }
}
