using Microsoft.Extensions.Logging;
using Superkatten.Application.View.Models;
using System;
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

        public async Task<IReadOnlyCollection<Superkat>> GetSuperkattenAsync()
        {
            return new List<Superkat>
            {
                new Superkat(1, "piet"),
                new Superkat(2, "jan"),
                new Superkat(3, "klaas")
            };
        }
    }
}