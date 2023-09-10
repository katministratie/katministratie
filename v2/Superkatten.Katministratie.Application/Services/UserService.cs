using Superkatten.Katministratie.Application.Contracts.Authenticate;
using Superkatten.Katministratie.Application.Contracts.Interfaces;
using Superkatten.Katministratie.Domain;
using Superkatten.Katministratie.Domain.Exceptions;
using Superkatten.Katministratie.Domain.Shared;

namespace Superkatten.Katministratie.Application.Services;

public class UserService : IUserService
{
    // TODO: not yet implemented

    public void Delete(string name)
    {
    }

    public User? Get(string name)
    {
        if (!name.Equals("Johan"))
        {
            return null;
        }

        return new User(name, "HASH") {
            Permissions = new List<UserPermissions> {
                UserPermissions.Administrator 
            } 
        };
    }

    public void Register(RegisterRequest model)
    {
    }

    public void Update(string name, UserUpdateRequest model)
    {
    }
}
