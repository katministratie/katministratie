using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Authenticate;
using Superkatten.Katministratie.Host.LocalStorage;
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

    public async Task<T?> Get<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request);
    }

    public async Task<T?> Post<T>(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
        };
        return await SendRequest<T>(request);
    }
    public async Task<T?> Post<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        return await SendRequest<T>(request);
    }
    public async Task<T?> Put<T>(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
        };
        return await SendRequest<T>(request);
    }

    public async Task Put(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
        };

        await SendRequest(request);
    }

    public async Task Delete(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        await SendRequest(request);
    }

    // helper methods

    private async Task SendRequest(HttpRequestMessage request)
    {
        var response = await SendWithAutorisationHeader(request);
        await CheckResponseForErrors(response);
    }

    private async Task<T?> SendRequest<T>(HttpRequestMessage request)
    {
        var response = await SendWithAutorisationHeader(request);
        await CheckResponseForErrors(response);

        return await response.Content.ReadFromJsonAsync<T>();
    }

    private async Task CheckResponseForErrors(HttpResponseMessage response)
    {
        // auto logout on 401 response
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("/");
            return;
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return;
        }

        // throw exception on error response
        if (!response.IsSuccessStatusCode)
        {
            var mediaType = response.Content?.Headers.ContentType?.MediaType;
            if (mediaType != "application/json")
            {
                throw new Exception($"unsuccesfull; response is: {response.StatusCode}");
            }
            
            if (response.Content is null)
            {
                throw new Exception("No content available, reason of failure unknown");
            }

            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception(error is not null
                        ? error["message"]
                        : "Fatal error"
                      );
        }
    }

    private async Task<HttpResponseMessage> SendWithAutorisationHeader(HttpRequestMessage request)
    {
        if (request.RequestUri is null)
        {
            throw new Exception("Request URI is empty");
        }

        // add jwt auth header if user is logged in and request is to the api url
        var user = await _localStorageService.GetItem<AuthenticateResponse>(LocalStorageItems.LOCALSTORAGE_ITEM_USER);
        if (user is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
        }

        return await _httpClient.SendAsync(request);
    }
}
