using Superkatten.Katministratie.Application.Contracts.Authenticate;
using Superkatten.Katministratie.Domain;

namespace Superkatten.Katministratie.Application.Contracts.Interfaces;

public interface IUserService
{
    void Register(RegisterRequest model);
    public User? Get(string name);
    void Update(string name, UserUpdateRequest model);
    void Delete(string name);
}
