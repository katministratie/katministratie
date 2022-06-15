using Superkatten.Katministratie.Contract.Authenticate;
using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;

namespace Superkatten.Katministratie.Application.Authenticate;

public interface IUserAuthorisationMapper
{
    AuthenticateResponse MapToAuthenticateResponse(User user, string token);
    User MapModelToUser(RegisterRequest model, string? passwordHash);
    User MapToDomain(int id, UpdateRequest model, string? passwordHash);
}
