using Superkatten.Katministratie.Contract.Authenticate;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.Authenticate;

public interface IUserAuthorisationMapper
{
    AuthenticateResponse MapToAuthenticateResponse(User user, string token);
    User MapModelToUser(RegisterRequest model, string? passwordHash);
    User MapModelToUser(int id, UpdateRequest model, string? passwordHash);
}
