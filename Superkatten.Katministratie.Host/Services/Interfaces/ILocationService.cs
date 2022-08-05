﻿using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Services.Interfaces;

public interface ILocationService
{
    Task<IReadOnlyCollection<Location>> GetLocationsAsync();
}
