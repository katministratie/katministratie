using Superkatten.Katministratie.Domain;
using Superkatten.Katministratie.Domain.Shared;

namespace Superkatten.Katministratie.Infrastructure.Services;

internal class UserRepository : IUserRepository
{
    // TODO: Temporary implementation

    public IReadOnlyCollection<User> GetAllUsers()
    {
        return new List<User>()
        {
            new User("Johan", string.Empty)
            {
                Permissions = new List<UserPermissions>
                {
                    UserPermissions.Administrator
                }
            }
        };
    }

    public User? GetUser(string userName)
    {
        return new User("Johan", string.Empty)
        {
            Permissions = new List<UserPermissions>
            {
                UserPermissions.Administrator
            }
        };
    }

    public void StoreUser(User user)
    {

    }

    public void UpdateUser(User user)
    {

    }
}
