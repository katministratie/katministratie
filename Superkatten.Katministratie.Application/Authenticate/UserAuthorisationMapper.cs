using Superkatten.Katministratie.Domain.Authenticate;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.Authenticate
{
    public class UserAuthorisationMapper : IUserAuthorisationMapper
    {
        public User MapModelToUser(UpdateRequest model)
        {
            throw new System.NotImplementedException();
        }

        public AuthenticateResponse MapToAuthenticateResponse(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
