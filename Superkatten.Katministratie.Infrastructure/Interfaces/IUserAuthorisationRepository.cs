using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;

namespace Superkatten.Katministratie.Infrastructure.Interfaces;

public interface IUserAuthorisationRepository
{
    IReadOnlyCollection<User> GetAllUsers();

    User? GetUserByName(string userName);

    void StoreUser(User user);

    void UpdateUser(User user);

    void DeleteUserById(int userId);
}
