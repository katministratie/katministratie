using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services.Http;
using System;

namespace Superkatten.Katministratie.Host.Services
{
    public class SuperkatActionService : ISuperkatActionService
    {
        private readonly IHttpService _httpService;

        public SuperkatActionService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public Task ToggleReserveSuperkatAsync(Guid superkatId)
        {
            var uri = $"api/SuperkatAction/ToggleReserve";
            return _httpService.Put(uri, superkatId);
        }

        public Task ToggleRetourSuperkatAsync(Guid superkatId)
        {
            var uri = $"api/SuperkatAction/ToggleRetour";
            return _httpService.Put(uri, superkatId);
        }

        public Task CreateSuperkatCageCardAsync(SuperkatCageCardPrintParameters parameters)
        {
            //var uri = "api/SuperkatAction/CreateSuperkatCageCard";
            //await _client.PutAsJsonAsync(uri, parameters);
            throw new NotImplementedException();
        }

        public Task ReserveSuperkatten(Guid gastgezinId, IReadOnlyCollection<Superkat> reservedSuperkattenParameters)
        {
            var uri = $"api/Adopting";
            var reserveParameters = new ReserveSuperkattenParameters
            {
                GastgezinId = gastgezinId,
                Superkatten = reservedSuperkattenParameters
            };

            return _httpService.Put(uri, reserveParameters);
        }
    }
}
