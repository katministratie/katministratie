using System.Net;
using System.Text.Json;

namespace Superkatten.Katministratie.Api.Client.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    public HttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> Get<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request);
    }


    private async Task<T?> SendRequest<T>(HttpRequestMessage request)
    {
        var response = await SendWithAutorisationHeader(request);
        await CheckResponse(response);

        return await response.Content.ReadFromJsonAsync<T>();
    }

    private async Task CheckResponse(HttpResponseMessage response)
    {
        // TODO: convert to JSON
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return;
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return;
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return;
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            // Opgegeven URI bestaat niet
            return;
        }

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var error = await response
                .Content
                .ReadFromJsonAsync<Dictionary<string, string>>();
        }
    }

    private async Task<HttpResponseMessage> SendWithAutorisationHeader(HttpRequestMessage request)
    {
        // TODO: add JWT header for authentication

        return await _httpClient.SendAsync(request);
    }
}
