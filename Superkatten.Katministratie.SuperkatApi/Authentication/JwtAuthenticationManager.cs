using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Superkatten.Katministratie.SuperkatApi.Authentication;

public class AuthenticationManager
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        IDictionary<string, string> _users = new Dictionary<string, string>
        {
            { "Johan", "test" },
            { "Peter", "0000" }
        };

        private readonly string _tokenKey;

        public JwtAuthenticationManager(string tokenKey)
        {
            _tokenKey = tokenKey;
        }

        public string Authenticate(string username, string password)
        {
            if (!_users.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
