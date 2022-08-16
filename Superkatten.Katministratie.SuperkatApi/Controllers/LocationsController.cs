using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Authorization;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain.Entities;

using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers;

[AuthorizeRoles(PermissionEnum.Administrator, PermissionEnum.Coordinator)]
[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;
    private readonly ILocationMapper _locationMapper;

    public LocationsController(ILocationService locationService, ILocationMapper locationMapper)
    {
        _locationService = locationService;
        _locationMapper = locationMapper;
    }

    [HttpGet]
    public IActionResult /*async Task<IReadOnlyCollection<ContractEntities.Location>> */GetAll()
    {
        var locations = _locationService.GetLocationsAsync();
        return Ok(locations);
//            .Select(_locationMapper.MapDomainToContract)
//            .ToList();
    }
}
