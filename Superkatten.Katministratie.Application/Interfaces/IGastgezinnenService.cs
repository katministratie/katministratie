﻿using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface IGastgezinnenService
    {
        Task<Gastgezin> CreateGastgezinAsync(CreateUpdateLocationNawParameters createGastgezinDtoParameters);
        Task<BaseLocation> UpdateLocationAsync(Guid locationId, CreateUpdateLocationNawParameters parameters)
        Task DeleteGastgezinAsync(Guid id);
        Task<IReadOnlyCollection<Gastgezin>> GetGastgezinnenAsync();
    }
}
