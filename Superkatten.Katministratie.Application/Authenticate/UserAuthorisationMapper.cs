using Superkatten.Katministratie.Contract.Authenticate;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.Authenticate
{
    public class UserAuthorisationMapper : IUserAuthorisationMapper
    {
        public User MapModelToUser(RegisterRequest model, string? passwordHash)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                PasswordHash = passwordHash
            };

            return user;
        }

        public User MapModelToUser(int id, UpdateRequest model, string? passwordHash)
        {
            var user = new User
            {
                Id = id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                PasswordHash = passwordHash
            };
            return user;
        }

        public AuthenticateResponse MapToAuthenticateResponse(User user)
        {
            var authenticateResponse = new AuthenticateResponse
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id
            };
            return authenticateResponse;
        }
    }
}
