using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Authenticate;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Superkatten.Katministratie.Host.Services.Authentication;

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorageService;

    private readonly IConfiguration _configuration;

    public HttpService(
        HttpClient httpClient,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService,
        IConfiguration configuration
    )
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        _configuration = configuration;
    }

    public async Task<T> Get<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request);
    }

    public async Task<T?> Post<T>(string uri, object? value)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        if (value is not null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        }

        return await SendRequest<T>(request);
    }

    // helper methods

    private async Task<T?> SendRequest<T>(HttpRequestMessage request)
    {
        if (request.RequestUri is null)
        {
            throw new Exception("Request URI is empty");
        }

        // add jwt auth header if user is logged in and request is to the api url
        var user = await _localStorageService.GetItem<AuthenticateResponse>("user");
        var isApiUrl = !request.RequestUri.IsAbsoluteUri;
        if (user is not null && isApiUrl)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
        }

        using var response = await _httpClient.SendAsync(request);

        // auto logout on 401 response
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("logout");
            return default;
        }

        // throw exception on error response
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception(error is not null 
                ? error["message"] 
                : "Fatal error"
            );
        }

        var result = await response.Content.ReadFromJsonAsync<T>();
        return result;
    }
}
