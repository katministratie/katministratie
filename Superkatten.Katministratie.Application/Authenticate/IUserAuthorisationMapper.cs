using Superkatten.Katministratie.Domain.Authenticate;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.Authenticate;

public interface IUserAuthorisationMapper
{
    AuthenticateResponse MapToAuthenticateResponse(User user);
    User MapModelToUser(UpdateRequest model);
}
