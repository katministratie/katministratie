using Superkatten.Katministratie.Domain;

namespace Superkatten.Katministratie.Infrastructure.Services;

public interface IUserRepository
{
    IReadOnlyCollection<User> GetAllUsers();
    User? GetUser(string userName);
    void StoreUser(User user);
    void UpdateUser(User user);

}
