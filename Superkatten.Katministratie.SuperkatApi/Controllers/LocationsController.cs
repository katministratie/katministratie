using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Authorization;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain.Entities;

using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers;

[AuthorizeRoles(PermissionEnum.Administrator, PermissionEnum.Coordinator)]
[Route("api/[controller]")]
[ApiController]
public class LocationsController
{
    private readonly ILocationService _locationService;
    private readonly ILocationMapper _locationMapper;

    public LocationsController(ILocationService locationService, ILocationMapper locationMapper)
    {
        _locationService = locationService;
        _locationMapper = locationMapper;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<ContractEntities.Location>> GetAll()
    {
        var locations = await _locationService.GetLocationsAsync();
        return locations
            .Select(_locationMapper.MapDomainToContract)
            .ToList();
    }
}
