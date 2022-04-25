using Superkatten.Katministratie.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.CageCard;
public class SuperkatCageCard : ISuperkatCageCard
{
    private readonly ISuperkattenService _superkatService;
    private readonly ICageCardComposer _cageCardComposer;

    public SuperkatCageCard(ISuperkattenService superkatService, ICageCardComposer cageCardComposer)
    {
        _superkatService = superkatService;
        _cageCardComposer = cageCardComposer;
    }

    public async Task CreateCageCardAsync(Guid id)
    {
        var superkat = await _superkatService.ReadSuperkatAsync(id);
        _cageCardComposer.Compose(superkat);
    }
}