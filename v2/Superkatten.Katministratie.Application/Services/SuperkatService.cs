using Superkatten.Katministratie.Application.Contracts.Entities;
using Superkatten.Katministratie.Application.Contracts.Interfaces;
using Superkatten.Katministratie.Application.Contracts.Parameters;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.Utils;
using Superkatten.Katministratie.Domain;
using Superkatten.Katministratie.Infrastructure.Services;

namespace Superkatten.Katministratie.Application.Services;

public class SuperkatService : ISuperkatService
{
    private readonly ISuperkattenRepository _superkattenRepository;
    private readonly ISuperkatMapper _superkatMapper;
    private readonly ISystemTime _systemTime;

    public SuperkatService(
        ISuperkattenRepository superkattenRepository,
        ISuperkatMapper superkatMapper,
        ISystemTime systemTime
    )
    {
        _superkattenRepository = superkattenRepository;
        _superkatMapper = superkatMapper;
        _systemTime = systemTime;
    }

    public async Task<List<SuperkatDto>> GetSuperkattenAsync()
    {
        var superkatten = await _superkattenRepository.GetSuperkattenAsync();

        var superkattenDto = superkatten
            .Select(_superkatMapper.MapFromDomain)
            .ToList();

        return superkattenDto ?? Array.Empty<SuperkatDto>().ToList();
    }

    public async Task<SuperkatDto?> CreateSuperkatAsync(NewSuperkatParameters newSuperkatParameters)
    {
        var entered = _systemTime.UtcNow;
        var maxSuperkatNumber = await _superkattenRepository.GetMaxSuperkatNumberForYear(entered.Year);

        var superkat = new Superkat(maxSuperkatNumber + 1, entered);

        await _superkattenRepository.CreateSuperkatAsync(superkat);
        
        return _superkatMapper.MapFromDomain(superkat);
    }
}
