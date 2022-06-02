using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Authenticate;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;

        public User? User { get; private set; }
        public bool IsAuthenticated => true; // User is not null;

        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task AuthenticateUserAsync(string username, string password)
        {
            var uri = $"api/users/authenticate";
            var authenticateRequest = new AuthenticateRequest
            {
                Username = username,
                Password = password
            };

            // Returns AuthenticateResponse
            var result = await _httpService.Post<AuthenticateResponse>(uri, authenticateRequest);
            User = new User
            {
                Name = result?.Name ?? string.Empty,
                Email = result?.Email ?? string.Empty,
                Username = result?.Username ?? string.Empty,
                Token = result?.Token ?? string.Empty,
                Id = result?.Id ?? -1
            };

            await _localStorageService.SetItem("user", User);
        }

        public async Task LogoutAsync()
        {
            User = null!;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("");
        }

        public async Task RegisterAsync(string username, string password, string name, string email)
        {
            var uri = $"api/Users/register";
            var registerRequest = new RegisterRequest
            {
                Username = username,
                Password = password,
                Name = name,
                Email = email,
            };

            // Returns AuthenticateResponse
            _ = await _httpService.Post<RegisterRequest>(uri, registerRequest);
        }
    }
}
