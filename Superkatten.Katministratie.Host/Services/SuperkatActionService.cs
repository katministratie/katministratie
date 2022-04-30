﻿using Superkatten.Katministratie.Host.Entities;
using System.Net.Http.Json;


namespace Superkatten.Katministratie.Host.Services
{
    public class SuperkatActionService : ISuperkatActionService
    {
        private readonly HttpClient _client;

        public SuperkatActionService(HttpClient client)
        {
            _client = client;
        }

        public async Task ToggleReserveSuperkatAsync(Guid superkatId)
        {
            var uri = $"api/SuperkatAction/ToggleReserve";
            _ = await _client.PutAsJsonAsync(uri, superkatId);
        }

        public async Task ToggleRetourSuperkatAsync(Guid superkatId)
        {
            var uri = $"api/SuperkatAction/ToggleRetour";
            _ = await _client.PutAsJsonAsync(uri, superkatId);
        }

        public async Task PrintSuperkatCageCardAsync(SuperkatCageCardPrintParameters parameters)
        {
            await _client.PutAsJsonAsync($"api/SuperkatAction/PrintSuperkatCageCard", parameters);
        }
    }
}
