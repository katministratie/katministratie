using Superkatten.Application.View.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Superkatten.Application.View.Services
{
    public class SuperkattenService : ISuperkattenService
    {
        private readonly HttpClient _httpClient;

        public SuperkattenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Superkat>> GetSuperkattenAsync()
        {
            var superkatten = await _httpClient.GetFromJsonAsync<Superkat[]>("api/superkatten");
            return superkatten;
        }
    }
}